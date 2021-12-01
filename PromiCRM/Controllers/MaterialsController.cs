﻿using AutoMapper;
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
    public class MaterialsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MaterialsController> _logger;

        public MaterialsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MaterialsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Get all records from materials table, convert to dto's
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMaterials()
        {
            var materials = await _unitOfWork.ProductMaterials.GetAll(includeProperties: "Product, MaterialWarehouse");
            var results = _mapper.Map<IList<ProductMaterialDTO>>(materials);
            return Ok(results);
        }
        /// <summary>
        /// Get record from materials table by id(primary key)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetMaterial")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMaterial(int id)
        {
            var material = await _unitOfWork.ProductMaterials.Get(m => m.Id == id, includeProperties: "Product, MaterialWarehouse");
            var result = _mapper.Map<ProductMaterialDTO>(material);
            return Ok(result);
        }
        /// <summary>
        /// GET all records from materials table by productId. convert to dto's
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("product/{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMaterialByProductId(int id)
        {
            var materials = await _unitOfWork.ProductMaterials.GetAll(m => m.ProductId == id, includeProperties: "Product, MaterialWarehouse");
            var results = _mapper.Map<IList<ProductMaterialDTO>>(materials);
            return Ok(results);
        }
        /// <summary>
        /// Check if model valid, convert to dto and insert
        /// </summary>
        /// <param name="materialDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMaterial([FromBody] CreateProductMaterialDTO materialDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateMaterial)}");
                return BadRequest("Submited invalid data");
            }
            var material = _mapper.Map<ProductMaterial>(materialDTO);
            await _unitOfWork.ProductMaterials.Insert(material);
            await _unitOfWork.Save();
            //call GetMaterial, provide material id, and material object. It will find created material and return to user
            return CreatedAtRoute("GetMaterial", new { id = material.Id }, material);
        }
        /// <summary>
        /// Check if model valid, check if exist & update it
        /// </summary>
        /// <param name="materialDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMaterial([FromBody] UpdateProductMaterialDTO materialDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMaterial)}");
                return BadRequest("Submited invalid data");
            }
            var material = await _unitOfWork.ProductMaterials.Get(m => m.Id == id);
            if (material == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMaterial)}");
                return BadRequest("Submited invalid data");
            }
            //convert materialDTO to material model. Put all values to material model 
            _mapper.Map(materialDTO, material);
            _unitOfWork.ProductMaterials.Update(material);
            await _unitOfWork.Save();
            return NoContent();
        }
        /// <summary>
        /// Check if exist and delete it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var material = await _unitOfWork.ProductMaterials.Get(m => m.Id == id);
            if (material == null)
            {
                _logger.LogError($"Invalid DELETE attemt in {nameof(DeleteMaterial)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.ProductMaterials.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
