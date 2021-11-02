using AutoMapper;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ServiceController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServices()
        {
            var services = await _unitOfWork.Services.GetAll();
            var results = _mapper.Map<IList<ServiceController>>(services);
            return Ok(results);
        }


        [HttpGet("{id:int}", Name = "GetService")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _unitOfWork.Services.Get(c => c.Id == id);
            var result = _mapper.Map<ServiceDTO>(service);
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSeervice([FromBody] CreateServiceDTO serviceDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateSeervice)}");
                return BadRequest("Submited invalid data");
            }
            var service = _mapper.Map<Service>(serviceDTO);
            await _unitOfWork.Services.Insert(service);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetService", new { id = service.Id }, service);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateService([FromBody] UpdateServiceDTO serviceDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateService)}");
                return BadRequest("Submited invalid data");
            }
            var service = await _unitOfWork.Services.Get(c => c.Id == id);
            if (service == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateService)}");
                return BadRequest("Submited invalid data");
            }


            _mapper.Map(serviceDTO, service);
            _unitOfWork.Services.Update(service);
            await _unitOfWork.Save();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = _unitOfWork.Services.Get(c => c.Id == id);
            if (service == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteService)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.Services.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
