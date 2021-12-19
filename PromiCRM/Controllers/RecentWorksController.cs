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
    [Route("api/[controller]")]
    [ApiController]
    public class RecentWorksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RecentWorksController> _logger;

        public RecentWorksController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RecentWorksController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecentWorks()
        {
            var recentWorks = await _unitOfWork.RecentWorks.GetAll(includeProperties: "User,Product");
            var results = _mapper.Map<IList<RecentWorkDTO>>(recentWorks);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetById")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var recentWork = await _unitOfWork.RecentWorks.Get(s => s.Id == id, includeProperties: "User,Product");
            var results = _mapper.Map<RecentWorkDTO>(recentWork);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRecentWork([FromBody] CreateRecentWorkDTO recentWorkDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateRecentWork)}");
                return BadRequest("Submited invalid data");
            }
            //map to product To model
            var recentWork = _mapper.Map<RecentWork>(recentWorkDTO);
            await _unitOfWork.RecentWorks.Insert(recentWork);
            await _unitOfWork.Save();
            var createdRecentWork= await _unitOfWork.RecentWorks.Get(s => s.Id == recentWork.Id, includeProperties: "User,Product");
            var result = _mapper.Map<RecentWorkDTO>(createdRecentWork);
            return Ok(result);
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
        public async Task<IActionResult> UpdateRecentWork(int id, [FromBody] UpdateRecentWorkDTO recentWorkDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateRecentWork)}");
                return BadRequest("Submited invalid data");
            }
            var recentWork = await _unitOfWork.RecentWorks.Get(s => s.Id == id);
            if (recentWork== null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateRecentWork)}");
                return BadRequest("Submited invalid data");
            }
            //map dto to SalesChannel model for db. Put all values from dto to model
            _mapper.Map(recentWorkDTO, recentWork);
            _unitOfWork.RecentWorks.Update(recentWork);
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
        public async Task<IActionResult> DeleteRecentWork(int id)
        {
            var recentWork= await _unitOfWork.RecentWorks.Get(s => s.Id == id);
            if (recentWork== null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteRecentWork)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.RecentWorks.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
