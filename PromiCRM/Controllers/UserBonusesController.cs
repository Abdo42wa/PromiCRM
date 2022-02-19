using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromiCore.IRepository;
using PromiCore.ModelsDTO;
using PromiData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserBonusesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserBonusesController> _logger;
        private readonly DatabaseContext _database;

        public UserBonusesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserBonusesController> logger, DatabaseContext database)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _database = database;
        }
        /// <summary>
        /// getting only this month user bonuses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersBonuses()
        {
            var today = DateTime.Now;
            var usersBonuses = await _database.UserBonuses.Include(b => b.User).Where(x => x.Date.Year == today.Year).Where(x => x.Date.Month == today.Month).ToListAsync();
            var results = _mapper.Map<IList<UserBonusDTO>>(usersBonuses);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetUserBonusById")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserBonusById(int id)
        {
            var usersBonus = await _unitOfWork.UserBonuses.Get(b => b.Id == id, includeProperties: "User");
            var results = _mapper.Map<UserBonusDTO>(usersBonus);
            return Ok(results);
        }

        [HttpGet("user/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserBonus(Guid id)
        {
            var usersBonus = await _unitOfWork.UserBonuses.Get(b => b.UserId == id, includeProperties: "User");
            var results = _mapper.Map<UserBonusDTO>(usersBonus);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUserBonus([FromBody]CreateUserBonusDTO userBonusDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateUserBonus)}");
                return BadRequest("Submited invalid data");
            }
            var userBonus = _mapper.Map<UserBonus>(userBonusDTO);
            await _unitOfWork.UserBonuses.Insert(userBonus);
            await _unitOfWork.Save();
            var createdBonus = await _unitOfWork.UserBonuses.Get(x => x.Id == userBonus.Id, includeProperties: "User");
            var result = _mapper.Map<UserBonusDTO>(createdBonus);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserBonus([FromBody]UpdateUserBonusDTO userBonusDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUserBonus)}");
                return BadRequest("Submited invalid data");
            }
            var userBonus = await _unitOfWork.UserBonuses.Get(x => x.Id == id);
            if(userBonus == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUserBonus)}");
                return BadRequest("Submited invalid data");
            }
            _mapper.Map(userBonusDTO, userBonus);
            _unitOfWork.UserBonuses.Update(userBonus);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUserService(int id)
        {
            var userBonus = await _unitOfWork.UserBonuses.Get(b => b.Id == id);
            if(userBonus == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteUserService)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.UserBonuses.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
