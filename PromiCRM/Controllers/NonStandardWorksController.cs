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
    public class NonStandardWorkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<NonStandardWorkController> _logger;

        public NonStandardWorkController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<NonStandardWorkController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNonStandardWorks()
        {
            var NonStandardWorks = await _unitOfWork.NonStandardWorks.GetAll();
            var results = _mapper.Map<IList<NonStandardWorkDTO>>(NonStandardWorks);
            return Ok(results);
        }


        [HttpGet("{id:int}", Name = "GetNonStandardWork")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNonStandardWork(int id)
        {
            var NonStandardWork = await _unitOfWork.NonStandardWorks.Get(c => c.Id == id);
            var result = _mapper.Map<NonStandardWorkDTO>(NonStandardWork);
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNonStandardWork([FromBody] CreateNonStandardWorksDTO createNonStandardWorksDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateNonStandardWork)}");
                return BadRequest("Submited invalid data");
            }
            var nonStandardWork = _mapper.Map<NonStandardWork>(createNonStandardWorksDTO);
            await _unitOfWork.NonStandardWorks.Insert(nonStandardWork);
            await _unitOfWork.Save();
            
            return CreatedAtRoute("GetNonStandardWork", new { id = nonStandardWork.Id }, nonStandardWork);
        }
       

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNonStandardWork([FromBody] UpdateNonStandardWorksDTO nonStandardWorksDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateNonStandardWork)}");
                return BadRequest("Submited invalid data");
            }
            var nonStandardWork = await _unitOfWork.NonStandardWorks.Get(c => c.Id == id);
            if (nonStandardWork == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateNonStandardWork)}");
                return BadRequest("Submited invalid data");
            }

            
            _mapper.Map(nonStandardWorksDTO, nonStandardWork);
            _unitOfWork.NonStandardWorks.Update(nonStandardWork);
            await _unitOfWork.Save();
            return NoContent();
        }

      
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteNStandardWork(int id)
        {
            var nonStandardWork = _unitOfWork.NonStandardWorks.Get(c => c.Id == id);
            if (nonStandardWork == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteNStandardWork)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.NonStandardWorks.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
