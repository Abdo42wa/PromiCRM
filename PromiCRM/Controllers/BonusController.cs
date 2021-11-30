using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class BonusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BonusController> _logger;

        public BonusController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BonusController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// GET all records from bonuses table. Map/convert to DTO's
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBonuses()
        {
            var bonuses = await _unitOfWork.Bonus.GetAll(includeProperties: "User");
            var results = _mapper.Map<IList<BonusDTO>>(bonuses);
            return Ok(results);
        }
        /// <summary>
        /// GET record from bonus table by Id(primary), map to DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetBonus")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBonus(int id)
        {
            var bonus = await _unitOfWork.Bonus.Get(b => b.Id == id, includeProperties: "User");
            var result = _mapper.Map<BonusDTO>(bonus);
            return Ok(result);
        }
        /// <summary>
        /// Get all records from bonus table by userId. Convert to dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("user/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBonusByUserId(Guid id)
        {
            var bonuses = await _unitOfWork.Bonus.GetAll(b => b.UserId == id);
            var result = _mapper.Map<IList<BonusDTO>>(bonuses);
            return Ok(result);
        }
        /// <summary>
        /// Check if model is valid. Convert to Bonus model and insert
        /// </summary>
        /// <param name="bonusDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBonus([FromBody]CreateBonusDTO bonusDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateBonus)}");
                return BadRequest("Submited data is invalid");
            }
            var bonus = _mapper.Map<Bonus>(bonusDTO);
            await _unitOfWork.Bonus.Insert(bonus);
            await _unitOfWork.Save();
            //created at route GetBonus. Passing Id that it needs to get record
            return CreatedAtRoute("GetBonus", new { id = bonus.Id}, bonus);
        }
        /// <summary>
        /// Check if model valid. Check if record exist. and update it
        /// </summary>
        /// <param name="bonusDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBonus([FromBody]UpdateBonusDTO bonusDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateBonus)}");
                return BadRequest("Submited data is invalid");
            }

            var bonus = await _unitOfWork.Bonus.Get(b => b.Id == id);
            if(bonus == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateBonus)}");
                return BadRequest("Submited data is invalid");
            }
            //convert bonusDTO to bonus model. Put all values from bonusDTO to bonus
            _mapper.Map(bonusDTO, bonus);
            _unitOfWork.Bonus.Update(bonus);
            await _unitOfWork.Save();

            return NoContent();
        }
        /// <summary>
        /// Check if record exist. Then delete it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBonus(int id)
        {
            var bonus = _unitOfWork.Bonus.Get(b => b.Id == id);
            if(bonus == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteBonus)}");
                return BadRequest("Submited data is invalid");
            }
            await _unitOfWork.Bonus.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
