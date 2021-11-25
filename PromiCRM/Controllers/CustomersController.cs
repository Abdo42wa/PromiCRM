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
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CustomersController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all records from customers table & convert them to dto's
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _unitOfWork.Customers.GetAll();
            var results = _mapper.Map<IList<CustomerDTO>>(customers);
            return Ok(results);
        }
        /// <summary>
        /// Get record from customers table by id, convert to dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetCustomer")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _unitOfWork.Customers.Get(c => c.Id == id); 
            var result = _mapper.Map<CustomerDTO>(customer);
            return Ok(result);
        }
        /// <summary>
        /// Check if model valid, convert to dto and insert
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateCustomer)}");
                return BadRequest("Submited invalid data");
            }
            var customer = _mapper.Map<Customer>(customerDTO);
            await _unitOfWork.Customers.Insert(customer);
            await _unitOfWork.Save();
            //calling GetCustomer & providing id for it from customer, then object. It'll return customer thats created
            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }
        /// <summary>
        /// Check if valid, check if exist & update it
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDTO customerDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCustomer)}");
                return BadRequest("Submited invalid data");
            }
            var customer = await _unitOfWork.Customers.Get(c => c.Id == id);
            if (customer == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCustomer)}");
                return BadRequest("Submited invalid data");
            }
            // convert customerDTO to customer model. put all values to customer
            _mapper.Map(customerDTO, customer);
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.Save();
            return NoContent();
        }
        /// <summary>
        /// Check if exist and delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = _unitOfWork.Customers.Get(c => c.Id == id);
            if (customer == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCustomer)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.Customers.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
