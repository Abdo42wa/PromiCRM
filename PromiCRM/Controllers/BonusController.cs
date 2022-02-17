using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromiCore.IRepository;
using PromiCore.ModelsDTO;
using PromiData.Models;
using System;
using System.Collections.Generic;
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
            var bonuses = await _unitOfWork.Bonuses.GetAll(includeProperties: "UserBonuses");
            var results = _mapper.Map<IList<BonusDTO>>(bonuses);
            return Ok(results);
        }
        /// <summary>
        /// GET record from bonus table by Id(primary), map to DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetBonus")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBonus(int id)
        {
            var bonus = await _unitOfWork.Bonuses.Get(b => b.Id == id, includeProperties: "UserBonuses");
            var result = _mapper.Map<BonusDTO>(bonus);
            return Ok(result);
        }
        /// <summary>
        /// Check if model is valid. Convert to Bonus model and insert
        /// </summary>
        /// <param name="bonusDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBonus([FromBody] CreateBonusDTO bonusDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateBonus)}");
                return BadRequest("Submited data is invalid");
            }
            var bonus = _mapper.Map<Bonus>(bonusDTO);
            await _unitOfWork.Bonuses.Insert(bonus);
            await _unitOfWork.Save();
            var createdBonus = await _unitOfWork.Bonuses.Get(x => x.Id == bonus.Id, includeProperties: "UserBonuses");
            var result = _mapper.Map<BonusDTO>(createdBonus);
            return Ok(result);
        }
        /// <summary>
        /// Check if model valid. Check if record exist. and update it
        /// </summary>
        /// <param name="bonusDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBonus([FromBody] UpdateBonusDTO bonusDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateBonus)}");
                return BadRequest("Submited data is invalid");
            }

            var bonus = await _unitOfWork.Bonuses.Get(b => b.Id == id);
            if (bonus == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateBonus)}");
                return BadRequest("Submited data is invalid");
            }
            //convert bonusDTO to bonus model. Put all values from bonusDTO to bonus
            _mapper.Map(bonusDTO, bonus);
            _unitOfWork.Bonuses.Update(bonus);
            await _unitOfWork.Save();

            return NoContent();
        }
        /// <summary>
        /// Check if record exist. Then delete it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBonus(int id)
        {
            var bonus = _unitOfWork.Bonuses.Get(b => b.Id == id);
            if (bonus == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteBonus)}");
                return BadRequest("Submited data is invalid");
            }
            await _unitOfWork.Bonuses.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
