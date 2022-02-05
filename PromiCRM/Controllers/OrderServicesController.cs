using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromiCRM.IRepository;
using PromiCRM.Models;
using PromiCRM.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderServicesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderServicesController> _logger;

        public OrderServicesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderServicesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderServices()
        {
            var orderServices = await _unitOfWork.OrderServices.GetAll();
            var results = _mapper.Map<IList<OrderServiceDTO>>(orderServices);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetOrderServiceById")]
       /* [Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var orderService = await _unitOfWork.OrderServices.Get(s => s.Id == id);
            var results = _mapper.Map<OrderServiceDTO>(orderService);
            return Ok(results);
        }
        /// <summary>
        /// map to model object for database. and insert it
        /// </summary>
        /// <param name="orderServiceDTO"></param>
        /// <returns></returns>
        [HttpPost]
        /*[Authorize(Roles = "ADMINISTRATOR")]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOrderService([FromBody] CreateOrderServiceDTO orderServiceDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateOrderService)}");
                return BadRequest("Submited invalid data");
            }
            //map to product To model
            var orderService = _mapper.Map<OrderService>(orderServiceDTO);
            await _unitOfWork.OrderServices.Insert(orderService);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetOrderServiceById", new { id = orderService.Id }, orderService);
        }
        /// <summary>
        /// Check if model valid. check if exist and update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderServiceDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        /*[Authorize(Roles = "ADMINISTRATOR")]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrderService(int id, [FromBody] UpdateOrderServiceDTO orderServiceDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrderService)}");
                return BadRequest("Submited invalid data");
            }
            var orderService = await _unitOfWork.OrderServices.Get(s => s.Id == id);
            if (orderService == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrderService)}");
                return BadRequest("Submited invalid data");
            }
            //map dto to OrderService model for db. Put all values from dto to model
            _mapper.Map(orderServiceDTO, orderService);
            _unitOfWork.OrderServices.Update(orderService);
            await _unitOfWork.Save();
            return NoContent();
        }

        /// <summary>
        /// Check if record exist, if so then delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        /*[Authorize(Roles = "ADMINISTRATOR")]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOrderService(int id)
        {
            var orderService = await _unitOfWork.OrderServices.Get(s => s.Id == id);
            if (orderService == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteOrderService)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.OrderServices.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }

    }
}
