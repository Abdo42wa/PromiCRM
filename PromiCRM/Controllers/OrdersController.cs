using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromiCRM.IRepository;
using PromiCRM.Models;
using PromiCRM.ModelsDTO;
using PromiCRM.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;
        public readonly IBlobService _blobService;
        public readonly DatabaseContext _database;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrdersController> logger, IBlobService blobService, DatabaseContext database)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _blobService = blobService;
            _database = database;
        }


        [HttpGet]
        /*[Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _unitOfWork.Orders.GetAll(includeProperties: "Product,User,Shipment,Customer,Country,Currency", orderBy: o => o.OrderByDescending(o => o.OrderFinishDate));
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }


        [HttpGet("{id:int}", Name = "GetOrder")]
        /*[Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _unitOfWork.Orders.Get(c => c.Id == id, includeProperties: "User,Shipment,Customer,Country,Currency");
            var result = _mapper.Map<OrderDTO>(order);
            return Ok(result);
        }
        /// <summary>
        /// Laukiantys gaminiai. all orders that are not completed
        /// </summary>
        /// <returns></returns>
        [HttpGet("main/pendingproducts")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingProducts()
        {
            var orders = await _database.Orders.Where(x => x.Status == false)
                .GroupBy(x => new { x.Status })
                .Select(o => new OrderDTO
                {
                    Quantity = o.Sum(o => o.Quantity)
                }).SingleOrDefaultAsync();
            return Ok(orders);
        }
        /// <summary>
        /// Butina siandien atlikti (main dashboard) all orders that are 
        /// not completed and their deadline is today
        /// or its late(meaning it was yesterday or .. week ago). summing quantities
        /// </summary>
        /// <returns></returns>
        [HttpGet("main/necessary/make/today")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersNecessaryTodayToMake()
        {
            var today = DateTime.Today;
            var orders = await _database.Orders.Where(o => o.Status == false)
                .Where(o => (o.OrderFinishDate.Date) <= today)
                .GroupBy(o => new { o.Status })
                .Select(o => new OrderDTO
                {
                    Quantity = o.Sum(o => o.Quantity)
                }).SingleOrDefaultAsync();
            return Ok(orders);
        }
        /// <summary>
        /// Siandien pagaminta (main dashboard).
        /// </summary>
        /// <returns></returns>
        [HttpGet("today/made/products")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodayMadeProducts()
        {
            var today = DateTime.Today;
            var orders = await _database.Orders.Where(o => o.Status == true)
                .Where(o => (o.CompletionDate.Value.Date) == today)
                .GroupBy(o => new { o.Status })
                .Select(o => new OrderDTO
                {
                    Quantity = o.Sum(o => o.Quantity)
                }).SingleOrDefaultAsync();
            //!!!!!!!!!!!!!!!!!! reikia prideti orderMadeDate. nes orderFinish date yra tiesiog deadline kada jis turi but padarytas
            return Ok(orders);
        }
        /// <summary>
        /// Nauji uzsakyti gaminiai (main dashboard). today made orders (no  completed).
        /// </summary>
        /// <returns></returns>
        [HttpGet("main/new/orders")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewOrders()
        {
            var today = DateTime.Today;
            var orders = await _database.Orders.Where(o => o.Status == false)
                .Where(o => (o.Date.Date) == today)
                .GroupBy(o => new { o.Status })
                .Select(o => new OrderDTO
                {
                    Quantity = o.Sum(o => o.Quantity)
                }).SingleOrDefaultAsync();
            return Ok(orders);
        }

        /// <summary>
        /// NOT FINISHED Express orders. image name i check first if orderImage is null means its "Standart"
        /// of "Sandelis" order then i can get image from Product. If order imagePath is not null thats "Ne-standartinis"
        /// </summary>
        /// <returns></returns>
        [HttpGet("express")]
        /*[Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUncompletedExpressOrders()
        {
            /*var orders = await _unitOfWork.Orders.GetAll(o => o.ShipmentTypeId == 1 && o.Status == false, orderBy: o => o.OrderByDescending(o => o.OrderFinishDate));*/
            var orders = await _database.Orders.Include(o => o.Product).
                Where(o => o.ShipmentTypeId == 1).
                Where(o => o.Status == false).
                Select(o => new OrderDTO
                {
                    OrderFinishDate = o.OrderFinishDate,
                    OrderNumber = o.OrderNumber,
                    Quantity = o.Quantity,
                    ProductCode = o.ProductCode,
                    ImagePath = o.ImagePath == null?o.Product.ImagePath:o.ImagePath,
                    Platforma = o.Platforma
                }).OrderByDescending(o => o.OrderFinishDate).ToListAsync();
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }
        /// <summary>
        /// not-standart orders for clients. NOT FINISHED
        /// </summary>
        /// <returns></returns>
        [HttpGet("clients")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotStandartOrdersForClients()
        {
            var orders = await _unitOfWork.Orders.GetAll(o => o.OrderType == "Ne-standartinis" && o.Status == false, includeProperties: "Customer,User", orderBy: o => o.OrderByDescending(o => o.OrderFinishDate));
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }
        /// <summary>
        /// Daugiausia nepagamintu produktu. Not completed standart orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("uncompleted")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUncompletedOrders()
        {
            var orders = await _database.Orders.Include(o => o.Product)
                .Where(o => o.Status == false)
                .Where(o => o.OrderType == "Standartinis")
                .GroupBy(o => new { o.ProductCode, o.Product.ImagePath })
                .Select(o => new OrderDTO
                {
                    ProductCode = o.Key.ProductCode,
                    ImagePath = o.Key.ImagePath,
                    Quantity = o.Sum(o => o.Quantity),
                }).OrderByDescending(o => o.Quantity).ToListAsync();
            return Ok(orders);
        }

        [HttpGet("planned/time")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUncompletedOrdersTime()
        {
            var orders = await _database.Orders.Include(o => o.Product)
                .Where(o => o.Status == false)
                .GroupBy(o => new { o.Status })
                .Select(o => new OrderDTO
                {
                    LaserTime = o.Sum(o => o.LaserTime * o.Quantity),
                    BondingTime = o.Sum(o => o.BondingTime * o.Quantity),
                    CollectionTime = o.Sum(o => o.CollectionTime * o.Quantity),
                    MilingTime = o.Sum(o => o.MilingTime * o.Quantity),
                    PaintingTime = o.Sum(o => o.PaintingTime * o.Quantity),
                    PackingTime = o.Sum(o => o.PackingTime * o.Quantity),
                    DoneLaserTime = (int)o.Sum(o => o.LaserUserId != null?o.LaserTime*o.Quantity:o.LaserTime*0),
                    DoneBondingTime = (int)o.Sum(o => o.BondingUserId != null ? o.BondingTime * o.Quantity : o.BondingTime * 0),
                    DoneCollectionTime = (int)o.Sum(o => o.CollectionUserId != null ? o.CollectionTime* o.Quantity : o.CollectionTime * 0),
                    DoneMilingTime = (int)o.Sum(o => o.MilingUserId!= null ? o.MilingTime * o.Quantity : o.MilingTime * 0),
                    DonePaintingTime = (int)o.Sum(o => o.PaintingUserId!= null ? o.PaintingTime* o.Quantity : o.PaintingTime * 0),
                    DonePackingTime = (int)o.Sum(o => o.PackingUserId!= null ? o.PackingTime* o.Quantity : o.PackingTime * 0)
                }).ToListAsync();
            return Ok(orders);
        }
        [HttpGet("warehouseUncompleted")]
/*        [Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUncompletedOrdersForWarehouse()
        {
            var orders = await _database.Orders.Include(o => o.Product)
                .Where(o => o.Status == false).Where(o => o.OrderType == "Sandelis")
                .GroupBy(x => new { x.ProductCode, x.Product.ImagePath })
                .Select(o => new OrderDTO
                {
                    ProductCode = o.Key.ProductCode,
                    ImagePath = o.Key.ImagePath,
                    Quantity = o.Sum(o => o.Quantity),
                    /*OrderFinishDate = o.Max(o => o.OrderFinishDate),*/
                    MinOrderFinishDate = o.Min(o => o.OrderFinishDate)
                }).OrderByDescending(o => o.Quantity).ToListAsync();

            return Ok(orders);
        }
        /// <summary>
        /// getting all completed orders. thats completed orders in past 5 weeks
        /// only completed "Standartinis" or "Ne-standartinis"
        /// </summary>
        /// <returns></returns>
        [HttpGet("weeksOrders")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersByWeeks()
        {
            //when grouping start from exactly one year ago
            var startDate = DateTime.Now.AddDays(-1 * 365);
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            var currentCulture = CultureInfo.CurrentCulture;
            DateTime today = DateTime.Now;
            DateTime fiveWeeksBefore = today.AddDays(-36);
            //s
            var orders = _database.Orders.AsEnumerable().Where(o => o.Status == true).
                Where(o => o.CompletionDate.Value.Date > fiveWeeksBefore.Date).
                Where(o => o.OrderType != "Ne-standartinis").
                GroupBy(o => cal.GetWeekOfYear(o.CompletionDate.Value.Date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek)).
                Select(x => new OrderDTO
                {
                    Quantity = x.Sum(x => x.Quantity),
                    CompletionDate = x.Max(x => x.CompletionDate.Value.Date),
                    WeekNumber = x.Key
                }).OrderBy(o => o.WeekNumber);
            //OrderByDescending(o => o.Quantity).ToListAsync();

            /*foreach (OrderDTO order in orders)
            {
                order.WeekNumber = cal.GetWeekOfYear(order.CompletionDate.Value.Date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            }*/
            return Ok(orders);
        }

        /// <summary>
        /// getting all completed orders in past Month(30 days).
        /// </summary>
        /// <returns></returns>
        [HttpGet("monthOrders")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMonthOrders()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            DateTime today = DateTime.Now;
            DateTime fiveWeeksBefore = today.AddDays(-30);
            //group by completion date. so for example each order of 2022/01/29 will be counted seper
            var orders = await _database.Orders.Where(o => o.Status == true).
                Where(o => o.CompletionDate.Value.Date > fiveWeeksBefore.Date).
                Where(o => o.OrderType != "Ne-standartinis").
                GroupBy(o => o.CompletionDate.Value.Date).Select(x => new OrderDTO
                {
                    Quantity = x.Sum(x => x.Quantity),
                    Id = x.Min(p => p.Id),
                    CompletionDate = x.Key,
                }).OrderByDescending(o => o.Quantity).ToListAsync();
            return Ok(orders);
        }

        [HttpGet("urgent")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUrgentOrders()
        {
            var orders = await _unitOfWork.Orders.GetAll(o => o.Status == false, o => o.OrderByDescending(o => o.OrderFinishDate).
            OrderBy(o => o.ProductCode),includeProperties: "Product,User,Shipment,Customer,Country,Currency");
            /*var orders = _database.Orders.Where(o => o.Status == false).
                OrderByDescending(o => o.OrderFinishDate).OrderBy(o => o.ProductCode).ToList();*/
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }
        /// <summary>
        /// Get newest 10 orders that were made. Ordering by date. Getting only "Sandelis" "Standartinis" orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("recent")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecentOrders()
        {
            var orders = await _database.Orders.Include(o => o.User).Include(o => o.Product)
                .Where(o => o.Status == true)
                .Where(o => o.OrderType != "Ne-standartinis")
                .OrderByDescending(o => o.CompletionDate)
                .Take(10).ToListAsync();
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }

        /// <summary>
        /// Create image. generate its name, get created image url from storage
        /// only then save order record to db
        /// </summary>
        /// <param name="createOrderDTO"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateOrder)}");
                return BadRequest("Submited invalid data");
            }
            var order = _mapper.Map<Order>(createOrderDTO);
            await _unitOfWork.Orders.Insert(order);

            //getting all all productMaterials with that productid, and grouping by materialWarehouseId
            //so to group same materials in one. sum quantities of same productMaterials
            var productMaterials = await _database.ProductMaterials.Where(p => p.ProductId == order.ProductId)
                .GroupBy(p => p.MaterialWarehouseId)
                .Select(x => new ProductMaterialDTO
                {
                    Quantity = x.Sum(x => x.Quantity),
                    ProductId = x.Min(x => x.ProductId),
                    MaterialWarehouseId = x.Min(x => x.MaterialWarehouseId)
                }).ToListAsync();

            var materialsWarehouse = await _database.MaterialsWarehouse.ToListAsync();
            foreach (ProductMaterialDTO material in productMaterials)
            {
                //getting each MaterialWarehouse obj by ProductMaterialDTO materialWarehouseId
                MaterialWarehouse ExistingWarehouseMaterial = materialsWarehouse.FirstOrDefault(m => m.Id == material.MaterialWarehouseId);
                //multiplying productMaterial.quantity from order quantity of products that we will make
                ExistingWarehouseMaterial.Quantity -= material.Quantity * order.Quantity;
                _unitOfWork.MaterialsWarehouse.Update(ExistingWarehouseMaterial);
            }
            //save made changes
            await _unitOfWork.Save();
            var createdOrder = await _unitOfWork.Orders.Get(o => o.Id == order.Id, includeProperties: "Product,User,Shipment,Customer,Country,Currency");
            var results = _mapper.Map<OrderDTO>(createdOrder);
            return Ok(results);
        }
        /// <summary>
        /// For Not-standart order we will add image
        /// </summary>
        /// <param name="createOrderDTO"></param>
        /// <returns></returns>
        [HttpPost("nonstandart")]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotStandart([FromBody] CreateOrderDTO createOrderDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateNotStandart)}");
                return BadRequest("Submited invalid data");
            }
            //FOR NOW WE DO NOT NEED IMAGE FOR NON STANDART ORDER

            /*if (createOrderDTO.File == null || createOrderDTO.File.Length < 1)
            {
                return BadRequest("Submited invalid data. Didnt get image");
            }*/

            /*var fileName = Guid.NewGuid() + Path.GetExtension(createOrderDTO.File.FileName);
            var imageUrl = await _blobService.UploadBlob(fileName, createOrderDTO.File, "productscontainer");
            createOrderDTO.ImageName = fileName;
            createOrderDTO.ImagePath = imageUrl;*/
            var order = _mapper.Map<Order>(createOrderDTO);
            await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.Save();
            var createdOrder = await _unitOfWork.Orders.Get(o => o.Id == order.Id, includeProperties: "User,Customer,ProductMaterials");
            var results = _mapper.Map<OrderDTO>(createdOrder);

            return Ok(results);
        }

        [HttpPost("warehouse")]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderAndWarehouse([FromForm] CreateOrderDTO createOrderDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateOrder)}");
                return BadRequest("Submited invalid data");
            }

            var order = _mapper.Map<Order>(createOrderDTO);
            await _unitOfWork.Orders.Insert(order);

            var warehouse = new WarehouseCounting
            {
                OrderId = order.Id,
                LastTimeChanging = DateTime.Now,
                QuantityProductWarehouse = order.Quantity
            };
            await _unitOfWork.WarehouseCountings.Insert(warehouse);



            await _unitOfWork.Save();

            var createdOrder = await _unitOfWork.Orders.Get(o => o.Id == order.Id, includeProperties: "Product,User,Shipment,Customer,Country,Currency");
            var results = _mapper.Map<OrderDTO>(createdOrder);

            return Ok(results);
        }

        /// <summary>
        /// update request when image(IFormFile) is not passed. just updating values
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
/*        [Authorize(Roles = "ADMINISTRATOR")]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDTO orderDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrder)}");
                return BadRequest("Submited invalid data");
            }
            var order = await _unitOfWork.Orders.Get(c => c.Id == id);
            if (order == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrder)}");
                return BadRequest("Submited invalid data");
            }

            _mapper.Map(orderDTO, order);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();
            return NoContent();
        }
        /// <summary>
        /// When packing is clicked, Non-Standart order is done. And we have to take materials from MaterialsWarehouse
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut("nonstandart/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> NonStandartFinishedUpdate([FromBody]UpdateOrderDTO orderDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(NonStandartFinishedUpdate)}");
                return BadRequest("Submited invalid data");
            }
            var order = await _unitOfWork.Orders.Get(o => o.Id == id);
            if(order == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(NonStandartFinishedUpdate)}");
                return BadRequest("Submited invalid data");
            }
            //put all values from dto to order model
            _mapper.Map(orderDTO, order);
            _unitOfWork.Orders.Update(order);

            //getting all all productMaterials with that orderId, and grouping by materialWarehouseId
            //so to group same materials in one. sum quantities of same productMaterials
            var productMaterials = await _database.ProductMaterials.Where(p => p.OrderId == order.Id)
                .GroupBy(p => p.MaterialWarehouseId)
                .Select(x => new ProductMaterialDTO
                {
                    Quantity = x.Sum(x => x.Quantity),
                    OrderId = x.Min(x => x.OrderId),
                    MaterialWarehouseId = x.Min(x => x.MaterialWarehouseId)
                }).ToListAsync();

            var materialsWarehouse = await _database.MaterialsWarehouse.ToListAsync();
            foreach (ProductMaterialDTO material in productMaterials)
            {
                //getting each MaterialWarehouse obj by ProductMaterialDTO materialWarehouseId
                MaterialWarehouse ExistingWarehouseMaterial = materialsWarehouse.FirstOrDefault(m => m.Id == material.MaterialWarehouseId);
                //multiplying productMaterial.quantity from order quantity of products that we will make
                ExistingWarehouseMaterial.Quantity -= material.Quantity * order.Quantity;
                _unitOfWork.MaterialsWarehouse.Update(ExistingWarehouseMaterial);
            }
            //save made changes
            await _unitOfWork.Save();
            var createdOrder = await _unitOfWork.Orders.Get(o => o.Id == order.Id, includeProperties: "Product,User,Shipment,Customer,Country,Currency");
            var results = _mapper.Map<OrderDTO>(createdOrder);
            return NoContent();
        }

        /// <summary>
        /// When we want to take from warehouse
        /// </summary>
        /// <param name="warehouseCountingDTO"></param>
        /// <returns></returns>
        [HttpPut("warehouse/subtract/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CollectProductFromWarehouse([FromBody] UpdateOrderDTO orderDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(CollectProductFromWarehouse)}");
                return BadRequest("Submited invalid data");
            }
            /* var quantity = int.Parse(orderDTO.WarehouseProductsNumber);*/
            var order = await _unitOfWork.Orders.Get(w => w.Id == id);
            if(order == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(CollectProductFromWarehouse)}");
                return BadRequest("Submited invalid data");
            }
            var warehouseCounting = await _unitOfWork.WarehouseCountings.Get(w => w.ProductCode == orderDTO.ProductCode);
            
            warehouseCounting.QuantityProductWarehouse -= orderDTO.WarehouseProductsNumber;
            warehouseCounting.LastTimeChanging = DateTime.Now;
            //map/convert orderDTO to order. all values its values go to order model
            _mapper.Map(orderDTO, order);
            //if after we take from warehouse product there its quantity is less or equal to 0 delete. else update
            if (warehouseCounting.QuantityProductWarehouse <= 0)
            {
                _unitOfWork.Orders.Update(order);
                await _unitOfWork.WarehouseCountings.Delete(warehouseCounting.Id);
                await _unitOfWork.Save();
                return NoContent();
            }
            else
            {
                _unitOfWork.Orders.Update(order);
                _unitOfWork.WarehouseCountings.Update(warehouseCounting);
                await _unitOfWork.Save();
                return NoContent();
            }
        }


       /* /// <summary>
        /// PUT request when passing object with image to update
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut("image/{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrderImage([FromForm] UpdateOrderDTO orderDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrderImage)}");
                return BadRequest("Submited invalid data");
            }
            if (orderDTO.File == null || orderDTO.File.Length < 1)
            {
                return BadRequest("Submited invalid data. Didnt get image");
            }
            //var fileName = Guid.NewGuid() + Path.GetExtension(warehouseMaterialForm.File.FileName);
            var fileName = Guid.NewGuid() + Path.GetExtension(MaterialWarehouseForm.File.FileName);
            var imageUrl = await _blobService.UploadBlob(orderDTO.ImageName, orderDTO.File, "productscontainer");
            orderDTO.ImagePath = imageUrl;

            var order = await _unitOfWork.Orders.Get(c => c.Id == id);
            if (order == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrderImage)}");
                return BadRequest("Submited invalid data");
            }

            _mapper.Map(orderDTO, order);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();
            return Ok(order);
        }*/


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = _unitOfWork.Orders.Get(c => c.Id == id);
            if (order == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteOrder)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.Orders.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }

    }
}
