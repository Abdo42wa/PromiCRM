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
    public class WarehouseCountingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<WarehouseCountingController> _logger;

        public WarehouseCountingController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WarehouseCountingController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWarehouseCountings ()
        {
            var warehouseCounting = await _unitOfWork.WarehouseCountings.GetAll();
            var result = _mapper.Map<IList<WarehouseCountingController>>(warehouseCounting);

            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetWarehouseCounting")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWarehouseCounting(int id)
        {
            var warehouseCounting = await _unitOfWork.WarehouseCountings.Get(w => w.Id == id);
            var result = _mapper.Map<WarehouseCountingDTO> (warehouseCounting);

            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatewarehouseCounting([FromBody] CreateWarehouseCountingDTO warehouseCountingDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreatewarehouseCounting)}");
                return BadRequest("Submited invalid data");
            }
            var warehouseCounting = _mapper.Map<WarehouseCounting>(warehouseCountingDTO);
            await _unitOfWork.WarehouseCountings.Insert(warehouseCounting);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetWarehouseCounting", new { id = warehouseCounting.Id }, warehouseCounting);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateWarehouseCounting([FromBody] UpdateWarehouseCountingDTO warehouseCountingDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWarehouseCounting)}");
                return BadRequest("Submited invalid data");
            }
            var warehouseCounting = await _unitOfWork.WarehouseCountings.Get(c => c.Id == id);
            if (warehouseCounting == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWarehouseCounting)}");
                return BadRequest("Submited invalid data");
            }


            _mapper.Map(warehouseCountingDTO, warehouseCounting);
            _unitOfWork.WarehouseCountings.Update(warehouseCounting);
            await _unitOfWork.Save();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteWarehouseCounting(int id)
        {
            var warehouseCounting = _unitOfWork.WarehouseCountings.Get(c => c.Id == id);
            if (warehouseCounting == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteWarehouseCounting)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.WarehouseCountings.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }

    }
}
