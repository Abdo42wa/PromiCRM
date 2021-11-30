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
    public class SalesChannelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SalesChannelsController> _logger;

        public SalesChannelsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SalesChannelsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSalesChannels()
        {
            var salesChannels = await _unitOfWork.SalesChannels.GetAll(includeProperties: "User");
            var results = _mapper.Map<IList<SalesChannelDTO>>(salesChannels);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetById")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var salesChannel = await _unitOfWork.SalesChannels.Get(s => s.Id == id, includeProperties: "User");
            var results = _mapper.Map<SalesChannelDTO>(salesChannel);
            return Ok(results);
        }
        /// <summary>
        /// map to model object for database. and insert it
        /// </summary>
        /// <param name="salesChannelDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSalesChannel([FromBody]CreateSalesChannelDTO salesChannelDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateSalesChannel)}");
                return BadRequest("Submited invalid data");
            }
            //map to product To model
            var salesChannel = _mapper.Map<SalesChannel>(salesChannelDTO);
            await _unitOfWork.SalesChannels.Insert(salesChannel);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetById", new { id = salesChannel.Id }, salesChannel);
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
        public async Task<IActionResult> UpdateSalesChannel(int id,[FromBody]UpdateSalesChannelDTO salesChannelDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateSalesChannel)}");
                return BadRequest("Submited invalid data");
            }
            var salesChannel = await _unitOfWork.SalesChannels.Get(s => s.Id == id);
            if(salesChannel == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateSalesChannel)}");
                return BadRequest("Submited invalid data");
            }
            //map dto to SalesChannel model for db. Put all values from dto to model
            _mapper.Map(salesChannelDTO, salesChannel);
            _unitOfWork.SalesChannels.Update(salesChannel);
            await _unitOfWork.Save();
            return NoContent();
        }

        /// <summary>
        /// Check if record exist, if so then delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSalesChannel(int id)
        {
            var salesChannel = await _unitOfWork.SalesChannels.Get(s => s.Id == id);
            if(salesChannel == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteSalesChannel)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.SalesChannels.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
