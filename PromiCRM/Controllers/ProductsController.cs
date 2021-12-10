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
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;
        private readonly DatabaseContext _database;
        private readonly IBlobService _blobService;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductsController> logger,DatabaseContext databaseContext, IBlobService blobService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _database = databaseContext;
            _blobService = blobService;
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            /*var products = await _unitOfWork.Products.GetAll(includeProperties: "MaterialWarehouse");*/
            var products = await _database.Products.Include(p => p.ProductMaterials).ThenInclude(d => d.MaterialWarehouse).ToListAsync();
            return Ok(products);
        }


        [HttpGet("{id:int}", Name = "GetProduct")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(c => c.Id == id, includeProperties: "Order,ProductMaterials");


            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }

        /// <summary>
        /// create image, then save record 
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateProduct)}");
                return BadRequest("Submited invalid data");
            }
            if(productDTO.File == null || productDTO.File.Length < 1)
            {
                return BadRequest("Submited invalid data. Didnt get image");
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(productDTO.File.FileName);
            var imageUrl = await _blobService.UploadBlob(fileName, productDTO.File, "productscontainer");
            productDTO.ImageName = fileName;
            productDTO.ImagePath = imageUrl;

            var product = _mapper.Map<Product>(productDTO);
            await _unitOfWork.Products.Insert(product);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }


        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDTO productDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProduct)}");
                return BadRequest("Submited invalid data");
            }
            var product = await _unitOfWork.Products.Get(c => c.Id == id);
            if (product == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProduct)}");
                return BadRequest("Submited invalid data");
            }


            _mapper.Map(productDTO, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
            return NoContent();
        }
        /// <summary>
        /// PUT request when passing object with image to update
        /// </summary>
        /// <param name="productDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("image/{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProductWithImage([FromForm]UpdateProductDTO productDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProductWithImage)}");
                return BadRequest("Submited invalid data");
            }
            if (productDTO.File == null || productDTO.File.Length < 1)
            {
                return BadRequest("Submited invalid data. Didnt get image");
            }
            /*var fileName = Guid.NewGuid() + Path.GetExtension(warehouseMaterialForm.File.FileName);*/
            var imageUrl = await _blobService.UploadBlob(productDTO.ImageName, productDTO.File, "productscontainer");
            productDTO.ImagePath = imageUrl;
            //get product by id
            var product = await _unitOfWork.Products.Get(c => c.Id == id);
            if (product == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProductWithImage)}");
                return BadRequest("Submited invalid data");
            }


            _mapper.Map(productDTO, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _unitOfWork.Products.Get(c => c.Id == id);
            if (product == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProduct)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.Products.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
