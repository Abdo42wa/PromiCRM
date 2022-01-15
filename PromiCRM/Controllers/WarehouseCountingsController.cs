using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromiCRM.IRepository;
using PromiCRM.Models;
using PromiCRM.ModelsDTO;
using PromiCRM.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseCountingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<WarehouseCountingsController> _logger;
        public readonly IBlobService _blobService;
        public readonly DatabaseContext _database;

        public WarehouseCountingsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WarehouseCountingsController> logger, IBlobService blobService, DatabaseContext database)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _blobService = blobService;
            _database = database;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWarehouseCountings()
        {
            var warehouseCounting = await _unitOfWork.WarehouseCountings.GetAll(includeProperties: "Order");
            var result = _mapper.Map<IList<WarehouseCountingDTO>>(warehouseCounting);

            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetWarehouseCounting")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWarehouseCounting(int id)
        {
            var warehouseCounting = await _unitOfWork.WarehouseCountings.Get(w => w.Id == id, includeProperties: "Order");
            var result = _mapper.Map<WarehouseCountingDTO>(warehouseCounting);

            return Ok(result);
        }

        [HttpGet("product/{productCode}")]
        public async Task<IActionResult> GetWarehouseProduct(string productCode)
        {
            var warehouseCounting = await _database.WarehouseCountings.Where(w => w.ProductCode == productCode).
                GroupBy(o => o.ProductCode).Select(x => new WarehouseCountingDTO
                {
                    ProductCode = x.Key,
                    QuantityProductWarehouse = x.Sum(p => p.QuantityProductWarehouse),
                    Id = x.Min(p => p.Id)
                }).OrderByDescending(o => o.QuantityProductWarehouse).FirstOrDefaultAsync();
            var result = _mapper.Map<WarehouseCountingDTO>(warehouseCounting);
            return Ok(result);

        }


        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatewarehouseCounting([FromForm] CreateWarehouseCountingDTO warehouseCountingDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreatewarehouseCounting)}");
                return BadRequest("Submited invalid data");
            }
            /*if(warehouseCountingDTO.File == null || warehouseCountingDTO.File.Length < 1) {
                return BadRequest("Submited invalid data. Didnt get image");
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(warehouseCountingDTO.File.FileName);
            var imageUrl = await _blobService.UploadBlob(fileName, warehouseCountingDTO.File, "productscontainer");
            warehouseCountingDTO.ImageName = fileName;
            warehouseCountingDTO.ImagePath = imageUrl;
*/
            var warehouseCounting = _mapper.Map<WarehouseCounting>(warehouseCountingDTO);
            await _unitOfWork.WarehouseCountings.Insert(warehouseCounting);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetWarehouseCounting", new { id = warehouseCounting.Id }, warehouseCounting);
        }


        /// <summary>
        /// regular update endpoint. updates values of found record
        /// </summary>
        /// <param name="warehouseCountingDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
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
        /// PUT request when passing object with image to update
        [HttpPut("image/{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateWarehouseCountingWithImage([FromForm] UpdateWarehouseCountingDTO warehouseCountingDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWarehouseCountingWithImage)}");
                return BadRequest("Submited invalid data");
            }
            /*  if (warehouseCountingDTO.File == null || warehouseCountingDTO.File.Length < 1)
              {
                  return BadRequest("Submited invalid data. Didnt get image");
              }
              var imageUrl = await _blobService.UploadBlob(warehouseCountingDTO.ImageName, warehouseCountingDTO.File, "productscontainer");
              warehouseCountingDTO.ImagePath = imageUrl;*/

            var warehouseCounting = await _unitOfWork.WarehouseCountings.Get(c => c.Id == id);
            if (warehouseCounting == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWarehouseCountingWithImage)}");
                return BadRequest("Submited invalid data");
            }


            _mapper.Map(warehouseCountingDTO, warehouseCounting);
            _unitOfWork.WarehouseCountings.Update(warehouseCounting);
            await _unitOfWork.Save();
            return Ok(warehouseCounting);
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
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
