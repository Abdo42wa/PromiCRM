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
    public class ShipmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ShipmentsController> _logger;

        public ShipmentsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ShipmentsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShipments()
        {
            var shipment = await _unitOfWork.Shipments.GetAll();
            var results = _mapper.Map<IList<ShipmentDTO>>(shipment);
            return Ok(results);
        }


        [HttpGet("{id:int}", Name = "GetShipment")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShipment(int id)
        {
            var shipment = await _unitOfWork.Shipments.Get(c => c.Id == id);
            var result = _mapper.Map<ShipmentDTO>(shipment);
            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShipment([FromBody] CreateShipmentDTO shipmentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateShipment)}");
                return BadRequest("Submited invalid data");
            }
            var shipment = _mapper.Map<Shipment>(shipmentDTO);
            await _unitOfWork.Shipments.Insert(shipment);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetShipment", new { id = shipment.Id }, shipment);
        }


        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateShipment([FromBody] UpdateShipmentDTO shipmentDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateShipment)}");
                return BadRequest("Submited invalid data");
            }
            var shipment = await _unitOfWork.Shipments.Get(c => c.Id == id);
            if (shipment == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateShipment)}");
                return BadRequest("Submited invalid data");
            }


            _mapper.Map(shipmentDTO, shipment);
            _unitOfWork.Shipments.Update(shipment);
            await _unitOfWork.Save();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var shipment = _unitOfWork.Shipments.Get(c => c.Id == id);
            if (shipment == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteShipment)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.Shipments.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
