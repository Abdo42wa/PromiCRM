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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GeWeeklyWorkSchedules()
        {
            var weeklyWorkSchedule = await _unitOfWork.WeeklyWorkSchedules.GetAll();
            var result = _mapper.Map<IList<WeeklyWorkScheduleDTO>>(weeklyWorkSchedule);

            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetWeeklyWorkSchedule")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWeeklyWorkSchedule(int id)
        {
            var weeklyWorkSchedule = await _unitOfWork.WeeklyWorkSchedules.Get(w => w.Id == id);
            var result = _mapper.Map<WeeklyWorkScheduleDTO>(weeklyWorkSchedule);

            return Ok(result);
        }


        [HttpPost]
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
            var weeklyWorkSchedule = _mapper.Map<WeeklyWorkSchedule>(weeklyWorkScheduleDTO);
            await _unitOfWork.WeeklyWorkSchedules.Insert(weeklyWorkSchedule);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetWeeklyWorkSchedule", new { id = weeklyWorkSchedule.Id }, weeklyWorkSchedule);
        }


        [HttpPut("{id:int}")]
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
