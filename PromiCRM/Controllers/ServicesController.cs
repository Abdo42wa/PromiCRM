using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromiCore.IRepository;
using PromiCore.ModelsDTO;
using PromiData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromiCRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ServicesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        /*[Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSalesChannels()
        {
            var services = await _unitOfWork.Services.GetAll();
            var results = _mapper.Map<IList<ServiceDTO>>(services);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetServiceById")]
        /*[Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _unitOfWork.Services.Get(s => s.Id == id);
            var results = _mapper.Map<ServiceDTO>(service);
            return Ok(results);
        }
        /// <summary>
        /// map to model object for database. and insert it
        /// </summary>
        /// <param name="salesChannelDTO"></param>
        /// <returns></returns>
        [HttpPost]
        /*[Authorize(Roles = "ADMINISTRATOR")]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDTO serviceDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateService)}");
                return BadRequest("Submited invalid data");
            }
            //map to product To model
            var service = _mapper.Map<Service>(serviceDTO);
            await _unitOfWork.Services.Insert(service);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetServiceById", new { id = service.Id }, service);
            /*return CreatedAtRoute("GetById", new { id = salesChannel.Id }, salesChannel);*/
        }
        /// <summary>
        /// Check if model valid. check if exist and update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salesChannelDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceDTO serviceDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateService)}");
                return BadRequest("Submited invalid data");
            }
            var service = await _unitOfWork.Services.Get(s => s.Id == id);
            if (service == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateService)}");
                return BadRequest("Submited invalid data");
            }
            //map dto to Service model for db. Put all values from dto to model
            _mapper.Map(serviceDTO, service);
            _unitOfWork.Services.Update(service);
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
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _unitOfWork.Services.Get(s => s.Id == id);
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
