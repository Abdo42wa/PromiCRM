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
            var orders = await _unitOfWork.Orders.GetAll(includeProperties: "User,Shipment,Customer,Country,Currency", orderBy: o => o.OrderByDescending(o => o.OrderFinishDate));
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
        /// NOT FINISHED Express orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("express")]
        /*[Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUncompletedExpressOrders()
        {
            var orders = await _unitOfWork.Orders.GetAll(o => o.ShipmentTypeId == 1 && o.Status == false, includeProperties: "User,Shipment,Customer,Country,Currency", orderBy: o => o.OrderByDescending(o => o.OrderFinishDate));
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }
        /// <summary>
        /// not-standart orders for clients. NOT FINISHED
        /// </summary>
        /// <returns></returns>
        [HttpGet("clientsOrders")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotStandartOrdersForClients()
        {
            var orders = await _unitOfWork.Orders.GetAll(o => o.OrderType == "Ne-standartinis" && o.Status == false, includeProperties: "Customer,User", orderBy: o => o.OrderByDescending(o => o.Date));
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }
        /// <summary>
        /// Not completed Not-standart and standart orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("uncompleted")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUncompletedOrders()
        {
            var orders = await _database.Orders.Include(o => o.Product)
                .Where(o => o.Status == false)
                .GroupBy(o => new { o.ProductCode, o.Product.ImagePath })
                .Select(o => new OrderDTO
                {
                    ProductCode = o.Key.ProductCode,
                    ImagePath = o.Key.ImagePath,
                    Quantity = o.Sum(o => o.Quantity),
                    OrderFinishDate = o.Max(o => o.OrderFinishDate),
                    MinOrderFinishDate = o.Min(o => o.OrderFinishDate)
                }).OrderByDescending(o => o.Quantity).ToListAsync();
            /*var products = await _unitOfWork.Products.GetAll();
            var orders = _database.Orders.Where(o => o.OrderType != "Sandelis").Where(o => o.Status == false).
                GroupBy(o => o.ProductCode).Select(x => new OrderDTO
                {
                    ProductCode = x.Key,
                    Quantity = x.Sum(x => x.Quantity),
                    Id = x.Min(p => p.Id),
                    UserId = x.Min(u => u.UserId),
                    OrderFinishDate = x.Max(o => o.OrderFinishDate)
                }).OrderByDescending(o => o.Quantity).ToList();

            foreach (OrderDTO order in orders)
            {
                var obj = products.FirstOrDefault(p => p.Code == order.ProductCode);
                if (obj != null && obj.ImagePath != null)
                {
                    order.ImagePath = obj.ImagePath;
                }
                else
                {
                    order.ImagePath = "";
                }

            }*/
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
                    OrderFinishDate = o.Max(o => o.OrderFinishDate),
                    MinOrderFinishDate = o.Min(o => o.OrderFinishDate)
                }).OrderByDescending(o => o.Quantity).ToListAsync();

            return Ok(orders);

            /*var products = await _unitOfWork.Products.GetAll();
            var orders = _database.Orders.Where(o => o.OrderType == "Sandelis").Where(o => o.Status == false).
                GroupBy(o => o.ProductCode).Select(x => new OrderDTO
                {
                    ProductCode = x.Key,
                    Quantity = x.Sum(x => x.Quantity),
                    Id = x.Min(p => p.Id),
                    UserId = x.Min(u => u.UserId),
                    OrderFinishDate = x.Max(o => o.OrderFinishDate)
                }).OrderByDescending(o => o.Quantity).ToList();*/
/*            foreach (OrderDTO order in orders)
            {
                var obj = products.FirstOrDefault(p => p.Code == order.ProductCode);
                if (obj != null && obj.ImagePath != null)
                {
                    order.ImagePath = obj.ImagePath;
                }
                else
                {
                    order.ImagePath = "";
                }

            }*/
            return Ok(orders);
        }
        /// <summary>
        /// getting all completed orders. and adding to each week of year
        /// </summary>
        /// <returns></returns>
        [HttpGet("weeksOrders")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersByWeeks()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            DateTime today = DateTime.Now;
            DateTime fiveWeeksBefore = today.AddDays(-36);

            var orders = await _database.Orders.Where(o => o.Status == true).
                Where(o => o.OrderFinishDate > fiveWeeksBefore).
                GroupBy(o => o.ProductCode).Select(x => new OrderDTO
                {
                    ProductCode = x.Key,
                    Quantity = x.Sum(x => x.Quantity),
                    Id = x.Min(p => p.Id),
                    UserId = x.Min(u => u.UserId),
                    OrderFinishDate = x.Max(o => o.OrderFinishDate)
                }).OrderByDescending(o => o.Quantity).ToListAsync();

            foreach (OrderDTO order in orders)
            {
                order.WeekNumber = cal.GetWeekOfYear(order.OrderFinishDate, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            }
            return Ok(orders);
        }

        /// <summary>
        /// getting all completed orders. and adding to each week of year
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

            var orders = await _database.Orders.Where(o => o.Status == true).
                Where(o => o.OrderFinishDate > fiveWeeksBefore).
                GroupBy(o => o.ProductCode).Select(x => new OrderDTO
                {
                    ProductCode = x.Key,
                    Quantity = x.Sum(x => x.Quantity),
                    Id = x.Min(p => p.Id),
                    UserId = x.Min(u => u.UserId),
                    OrderFinishDate = x.Max(o => o.OrderFinishDate)
                }).OrderByDescending(o => o.Quantity).ToListAsync();
            return Ok(orders);
        }

        [HttpGet("urgent")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersForWarehouseThatCompleted()
        {
            var orders = await _unitOfWork.Orders.GetAll(o => o.Status == false, o => o.OrderByDescending(o => o.OrderFinishDate).
            OrderBy(o => o.ProductCode),includeProperties: "Product,User,Shipment,Customer,Country,Currency");
            /*var orders = _database.Orders.Where(o => o.Status == false).
                OrderByDescending(o => o.OrderFinishDate).OrderBy(o => o.ProductCode).ToList();*/
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
           /* if (createOrderDTO.File == null || createOrderDTO.File.Length < 1)
            {
                return BadRequest("Submited invalid data. Didnt get image");
            }*/
         /*   var fileName = Guid.NewGuid() + Path.GetExtension(createOrderDTO.File.FileName);
            var imageUrl = await _blobService.UploadBlob(fileName, createOrderDTO.File, "productscontainer");
            createOrderDTO.ImageName = fileName;
            createOrderDTO.ImagePath = imageUrl;*/

            var order = _mapper.Map<Order>(createOrderDTO);
            await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
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
           /* if (createOrderDTO.File == null || createOrderDTO.File.Length < 1)
            {
                return BadRequest("Submited invalid data. Didnt get image");
            }*/
           /* var fileName = Guid.NewGuid() + Path.GetExtension(createOrderDTO.File.FileName);
            var imageUrl = await _blobService.UploadBlob(fileName, createOrderDTO.File, "productscontainer");
            createOrderDTO.ImageName = fileName;
            createOrderDTO.ImagePath = imageUrl;*/

            var order = _mapper.Map<Order>(createOrderDTO);
            await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.Save();

            var warehouse = new WarehouseCounting
            {
                OrderId = order.Id,
                LastTimeChanging = DateTime.Now,
                QuantityProductWarehouse = order.Quantity
            };
            await _unitOfWork.WarehouseCountings.Insert(warehouse);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
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
        /// PUT request when passing object with image to update
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
       /* [HttpPut("image/{id:int}")]
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
            *//*if (orderDTO.File == null || orderDTO.File.Length < 1)
            {
                return BadRequest("Submited invalid data. Didnt get image");
            }
            *//*var fileName = Guid.NewGuid() + Path.GetExtension(warehouseMaterialForm.File.FileName);*//*
            var imageUrl = await _blobService.UploadBlob(orderDTO.ImageName, orderDTO.File, "productscontainer");
            orderDTO.ImagePath = imageUrl;

            var order = await _unitOfWork.Orders.Get(c => c.Id == id);
            if (order == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrderImage)}");
                return BadRequest("Submited invalid data");
            }*//*

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
