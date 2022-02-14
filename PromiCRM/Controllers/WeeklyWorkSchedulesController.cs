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
using System.Globalization;
using System.Threading.Tasks;

namespace PromiCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeklyWorkSchedulesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<WeeklyWorkSchedulesController> _logger;

        public WeeklyWorkSchedulesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WeeklyWorkSchedulesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GeWeeklyWorkSchedules()
        {
            var weeklyWorkSchedule = await _unitOfWork.WeeklyWorkSchedules.GetAll(includeProperties: "User");
            var result = _mapper.Map<IList<WeeklyWorkScheduleDTO>>(weeklyWorkSchedule);

            return Ok(result);
        }
        /// <summary>
        /// Get only this week schedule. We do not need other 
        /// </summary>
        /// <returns></returns>
        [HttpGet("works")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWeeksWorks()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            DateTime today = DateTime.Now;
            DateTime weekBefore = today.AddDays(-7);

            var weeklyWorkSchedules = await _unitOfWork.WeeklyWorkSchedules.GetAll(o => o.Date > weekBefore, includeProperties: "User");
            var results = _mapper.Map<IList<WeeklyWorkScheduleDTO>>(weeklyWorkSchedules);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetWeeklyWorkSchedule")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWeeklyWorkSchedule(int id)
        {
            var weeklyWorkSchedule = await _unitOfWork.WeeklyWorkSchedules.Get(w => w.Id == id, includeProperties: "User");
            var result = _mapper.Map<WeeklyWorkScheduleDTO>(weeklyWorkSchedule);

            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateWeeklyWorkSchedule([FromBody] CreateWeeklyWorkScheduleDTO weeklyWorkScheduleDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateWeeklyWorkSchedule)}");
                return BadRequest("Submited invalid data");
            }
            weeklyWorkScheduleDTO.Date = DateTime.Now;
            var weeklyWorkSchedule = _mapper.Map<WeeklyWorkSchedule>(weeklyWorkScheduleDTO);
            await _unitOfWork.WeeklyWorkSchedules.Insert(weeklyWorkSchedule);
            await _unitOfWork.Save();

            var createdWork = await _unitOfWork.WeeklyWorkSchedules.Get(w => w.Id == weeklyWorkSchedule.Id, includeProperties: "User");
            var result = _mapper.Map<WeeklyWorkScheduleDTO>(createdWork);


            return Ok(result);
        }


        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateWeeklyWorkSchedule([FromBody] UpdateWeeklyWorkScheduleDTO weeklyWorkScheduleDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWeeklyWorkSchedule)}");
                return BadRequest("Submited invalid data");
            }
            var weeklyWorkSchedule = await _unitOfWork.WeeklyWorkSchedules.Get(c => c.Id == id);
            if (weeklyWorkSchedule == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWeeklyWorkSchedule)}");
                return BadRequest("Submited invalid data");
            }


            _mapper.Map(weeklyWorkScheduleDTO, weeklyWorkSchedule);
            _unitOfWork.WeeklyWorkSchedules.Update(weeklyWorkSchedule);
            await _unitOfWork.Save();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteWorkSchedule(int id)
        {
            var weeklyWorkSchedule = _unitOfWork.WeeklyWorkSchedules.Get(c => c.Id == id);
            if (weeklyWorkSchedule == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteWorkSchedule)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.WeeklyWorkSchedules.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
