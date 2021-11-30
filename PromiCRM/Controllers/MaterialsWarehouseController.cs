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
    public class MaterialsWarehouseController : ControllerBase
    {
        /*initializing iunitofwork*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MaterialsWarehouseController> _logger;

        public MaterialsWarehouseController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MaterialsWarehouseController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWarehouseMaterials()
        {
            var materials = await _unitOfWork.MaterialsWarehouse.GetAll();
            var results = _mapper.Map<IList<MaterialWarehouseDTO>>(materials);
            return Ok(results);
        }

        [HttpGet("{id:int}",Name = "GetWarehouseMaterial")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWarehouseMaterial(int id)
        {
            var material = await _unitOfWork.MaterialsWarehouse.Get(m => m.Id == id);
            var result = _mapper.Map<MaterialWarehouseDTO>(material);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMaterial([FromBody]CreateMaterialWarehouseDTO materialWarehouseDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateMaterial)}");
                return BadRequest("Submited invalid data");
            }
            var material = _mapper.Map<MaterialWarehouse>(materialWarehouseDTO);
            await _unitOfWork.MaterialsWarehouse.Insert(material);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetWarehouseMaterial", new { id = material.Id }, material);
        }

        /// <summary>
        /// check if exist, if so update
        /// </summary>
        /// <param name="materialWarehouseDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateWarehouseMaterial([FromBody] UpdateMaterialWarehouseDTO materialWarehouseDTO, int id) {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWarehouseMaterial)}");
                return BadRequest("Submited invalid data");
            }
            var material = await _unitOfWork.MaterialsWarehouse.Get(m => m.Id == id);
            if(material == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWarehouseMaterial)}");
                return BadRequest("Submited invalid data");
            }
            //map dto to domain obj. put all values from dto to domain obj
            _mapper.Map(materialWarehouseDTO, material);
            _unitOfWork.MaterialsWarehouse.Update(material);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var material = await _unitOfWork.MaterialsWarehouse.Get(m => m.Id == id);
            if(material == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMaterial)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.MaterialsWarehouse.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }

    }
}
