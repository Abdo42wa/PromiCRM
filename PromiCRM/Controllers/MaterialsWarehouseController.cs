using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromiCore.IRepository;
using PromiCore.ModelsDTO;
using PromiCore.Services;
using PromiData.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public readonly IBlobService _blobService;

        public MaterialsWarehouseController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MaterialsWarehouseController> logger, IBlobService blobService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _blobService = blobService;
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

        [HttpGet("{id:int}", Name = "GetWarehouseMaterial")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWarehouseMaterial(int id)
        {
            var material = await _unitOfWork.MaterialsWarehouse.Get(m => m.Id == id);
            var result = _mapper.Map<MaterialWarehouseDTO>(material);
            return Ok(result);
        }
        /// <summary>
        /// create image. then pass created image url and name to material obj
        /// and then can add new record to db
        /// </summary>
        /// <param name="warehouseMaterialForm"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMaterial([FromForm] MaterialWarehouseForm warehouseMaterialForm)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateMaterial)}");
                return BadRequest("Submited invalid data");
            }
            /*if (warehouseMaterialForm.File == null || warehouseMaterialForm.File.Length < 1)
            {
                return BadRequest("Submited invalid data. Didnt get image");
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(warehouseMaterialForm.File.FileName);
            var imageUrl = await _blobService.UploadBlob(fileName, warehouseMaterialForm.File, "materials");
            warehouseMaterialForm.ImageName = fileName;
            warehouseMaterialForm.ImagePath = imageUrl;*/
            var material = _mapper.Map<MaterialWarehouse>(warehouseMaterialForm);
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
        public async Task<IActionResult> UpdateWarehouseMaterial([FromBody] UpdateMaterialWarehouseDTO materialWarehouseDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWarehouseMaterial)}");
                return BadRequest("Submited invalid data");
            }
            var material = await _unitOfWork.MaterialsWarehouse.Get(m => m.Id == id);
            if (material == null)
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
        /// <summary>
        /// PUT request when passing object with image to update
        /// </summary>
        /// <param name="warehouseMaterialForm"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("image/{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateWarehouseMaterialWithImage([FromForm] MaterialWarehouseForm warehouseMaterialForm, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWarehouseMaterialWithImage)}");
                return BadRequest("Submited invalid data");
            }
            if (warehouseMaterialForm.File == null || warehouseMaterialForm.File.Length < 1)
            {
                return BadRequest("Submited invalid data. Didnt get image");
            }
            /*var fileName = Guid.NewGuid() + Path.GetExtension(warehouseMaterialForm.File.FileName);*/
            var imageUrl = await _blobService.UploadBlob(warehouseMaterialForm.ImageName, warehouseMaterialForm.File, "materials");
            warehouseMaterialForm.ImagePath = imageUrl;
            //get material by id
            var material = await _unitOfWork.MaterialsWarehouse.Get(m => m.Id == id);
            if (material == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWarehouseMaterial)}");
                return BadRequest("Submited invalid data");
            }

            _mapper.Map(warehouseMaterialForm, material);
            _unitOfWork.MaterialsWarehouse.Update(material);
            await _unitOfWork.Save();
            return Ok(material);
        }

        [HttpPut("update")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMany([FromBody] IList<MaterialWarehouseDTO> materialsDTO)
        {
            var materialsWarehouse = _mapper.Map<IList<MaterialWarehouse>>(materialsDTO);
            _unitOfWork.MaterialsWarehouse.UpdateRange(materialsWarehouse);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var material = await _unitOfWork.MaterialsWarehouse.Get(m => m.Id == id);
            if (material == null)
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
