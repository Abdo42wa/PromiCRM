using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromiCore.IRepository;
using PromiCore.ModelsDTO;
using PromiCore.Services;
using PromiData.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrdersController> logger, IBlobService blobService, DatabaseContext database, IOrdersRepository ordersRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _blobService = blobService;
            _database = database;
            _ordersRepository = ordersRepository;
        }

        //getting all orders that are not not-standart
        [HttpGet]
        /*[Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _ordersRepository.GetOrders();
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }
        //getting only not-standart orders
        [HttpGet("nonstandart")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNonStandartOrders()
        {
            var orders = await _ordersRepository.GetNonStandartOrders();
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _ordersRepository.GetOrderById(id);
            var result = _mapper.Map<OrderDTO>(order);
            return Ok(result);
        }

        [HttpGet("nonstandart/{id:int}", Name = "GetOrder")]
        /*[Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNonStandartOrderById(int id)
        {
            var order = await _ordersRepository.GetNonStandartOrderById(id);
            var result = _mapper.Map<OrderDTO>(order);
            return Ok(result);
        }

        [HttpGet("biggest/orderNumber")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderWithBiggestOrderNumber()
        {
            var orderNumber = await _ordersRepository.GetOrderWithBiggestOrderNumber();
            return Ok(orderNumber);
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
            var order = await _ordersRepository.GetPendingProducts();
            return Ok(order);
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
        public async Task<IActionResult> GetOrdersNecessaryToMake()
        {
            var order = await _ordersRepository.GetOrdersNecessaryToMake();
            return Ok(order);
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
            var order = await _ordersRepository.GetTodayMadeProducts();
            //!!!!!!!!!!!!!!!!!! reikia prideti orderMadeDate. nes orderFinish date yra tiesiog deadline kada jis turi but padarytas
            return Ok(order);
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
            var order = await _ordersRepository.GetNewOrders();
            return Ok(order);
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
            var orders = await _ordersRepository.GetUncompletedExpressOrders();
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
        public async Task<IActionResult> GetNonStandartOrdersForClients()
        {
            var orders = await _ordersRepository.GetNonStandartOrdersForClients();
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
        public async Task<IActionResult> GetMostUncompletedOrdersProducts()
        {
            var orders = await _ordersRepository.GetMostUncompletedOrdersProducts();
            return Ok(orders);
        }

        [HttpGet("planned/time")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUncompletedOrdersTime()
        {
            var orders = await _ordersRepository.GetUncompletedOrdersTime();
            return Ok(orders);
        }

        /// <summary>
        /// Neisiustu siuntiniu lentele
        /// </summary>
        /// <returns></returns>
        [HttpGet("unsended")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnsendedOrders()
        {
            var orders = await _ordersRepository.GetUnsendedOrders();
            return Ok(orders);
        }


        /// <summary>
        /// Rekomenduojama gaminti
        /// </summary>
        /// <returns></returns>
        [HttpGet("recommendedforproduction")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecommendedForProductionOrders()
        {
            var orders = await _ordersRepository.GetRecommendedForProductionOrders();
            return Ok(orders);
        }

        [HttpGet("warehouseUncompleted")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUncompletedOrdersForWarehouse()
        {
            var orders = await _ordersRepository.GetUncompletedOrdersForWarehouse();
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
            var orders = await _ordersRepository.GetMonthOrders();
            return Ok(orders);
        }

        /// <summary>
        /// Employee made orders through whole month
        /// </summary>
        /// <returns></returns>
        [HttpGet("employee/products")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeOrders()
        {
            var userServices = await _ordersRepository.GetEmployeeOrders();
            return Ok(userServices);
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
            var orders = await _ordersRepository.GetOrdersByWeeks();
            return Ok(orders);
        }

        [HttpGet("urgent")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUrgentOrders()
        {
            var orders = await _unitOfWork.Orders.GetAll(o => o.Status == false, o => o.OrderByDescending(o => o.OrderFinishDate).
            OrderBy(o => o.ProductCode), includeProperties: "Product,User,Shipment,Customer,Country");
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
            var orders = await _ordersRepository.GetRecentOrders();
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }

        /// ATASKAITOS - REPORTS - --------------------------------------

        [HttpGet("reports/last-month/sold")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLastMonthSoldProducts()
        {
            var standartOrders = await _ordersRepository.GetLastMonthSoldProducts();
            var nonStandartOrders = await _ordersRepository.GetLastMonthNonStandartSoldProducts();
            var results = standartOrders.Concat(nonStandartOrders);
            return Ok(results);
        }
        //Atvaizdavimas pagal platforma kiek uzsakyta ir labiausiai veluojantys is tu eiles tvarka
        [HttpGet("uncompleted/by-platform")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUncompleteOrdersByPlatforms()
        {
            var orders = await _ordersRepository.GetUncompletedOrdersByPlatforms();
            return Ok(orders);
        }

        //getting orders grouped by platforms in specified period of time(between to dates)
        [HttpGet("reports/completed/platforms")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompletedPlatformsOrdersByTime([FromQuery]string dateFrom, [FromQuery]string dateTo)
        {
            var fromDate = DateTime.Parse(dateFrom);
            var toDate = DateTime.Parse(dateTo);
            var orders = await _ordersRepository.GetCompletedPlatformsOrdersByTime(fromDate,toDate);
            return Ok(orders);
        }

        //Ataskaita pagal pasirinkta salis ir laikotarpi
        [HttpGet("reports/completed/countrys")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompletedCountryOrdersByTime([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            var fromDate = DateTime.Parse(dateFrom);
            var toDate = DateTime.Parse(dateTo);
            var orders = await _ordersRepository.GetOrdersByTimeAndCountry(fromDate, toDate);
            return Ok(orders);
        }


        //Populiariausiu prekiu ataskaita per pasirinkta laikas
        [HttpGet("reports/popular/product")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPopularProductByTime([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            var fromDate = DateTime.Parse(dateFrom);
            var toDate = DateTime.Parse(dateTo);
            var orders = await _ordersRepository.GetPopularProductByTime(fromDate, toDate);
            return Ok(orders);
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
            /*var createdOrder = await _unitOfWork.Orders.Get(o => o.Id == order.Id, includeProperties: "Product,User,Shipment,Customer,Country");*/
            var createdOrder = await _database.Orders.Where(o => o.Id == order.Id).
                Include(o => o.User).
                Include(o => o.Shipment).
                Include(o => o.Customer).
                Include(o => o.Country).
                Include(o => o.Product).
                ThenInclude(o => o.OrderServices).
                ThenInclude(p => p.Service).
                AsNoTracking().
                FirstOrDefaultAsync();
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
            var createdOrder = await _database.Orders.Where(o => o.Id == order.Id).
                Include(o => o.User).
                Include(o => o.Shipment).
                Include(o => o.Customer).
                Include(o => o.Country).
                Include(o => o.OrderServices).
                ThenInclude(p => p.Service).
                OrderByDescending(o => o.OrderFinishDate).
                AsNoTracking().
                FirstOrDefaultAsync();
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
        /// when user selects that PACKING is done. it will update UserServices 
        /// and update Order to status true(complete)
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut("warehouse/standart/finished/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrderAndComplete([FromBody] UpdateOrderDTO orderDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrderAndComplete)}");
                return BadRequest("Submited invalid data");
            }
            var order = await _unitOfWork.Orders.Get(o => o.Id == id);
            if (order == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrderAndComplete)}");
                return BadRequest("Submited invalid data");
            }
            _mapper.Map(orderDTO, order);
            _unitOfWork.UserServices.UpdateRange(order.UserServices);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();
            return Ok(order.UserServices);

        }

        [HttpPut("nonstandart/{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNonStandartOrder([FromBody] UpdateOrderDTO orderDTO, int id)
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
            if(order.OrderServices.Count > 0)
                _database.OrderServices.UpdateRange(order.OrderServices);
            _database.Orders.Update(order);
            await _unitOfWork.Save();
            return Ok(order.OrderServices);
        }


        /// <summary>
        /// When packing is clicked, Non-Standart order is done. And we have to take materials from MaterialsWarehouse
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut("nonstandart/finished/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> NonStandartFinishedUpdate([FromBody] UpdateOrderDTO orderDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(NonStandartFinishedUpdate)}");
                return BadRequest("Submited invalid data");
            }
            var order = await _unitOfWork.Orders.Get(o => o.Id == id);
            if (order == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(NonStandartFinishedUpdate)}");
                return BadRequest("Submited invalid data");
            }
            //put all values from dto to order model
            _mapper.Map(orderDTO, order);
            _unitOfWork.UserServices.UpdateRange(order.UserServices);
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

            return Ok(order.UserServices);
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
            if (order == null)
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
