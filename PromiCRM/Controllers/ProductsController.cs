using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;
        private readonly DatabaseContext _database;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductsController> logger,DatabaseContext databaseContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _database = databaseContext;
        }


        [HttpGet]
        /*[Authorize]*/
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            /*var products = await _unitOfWork.Products.GetAll(includeProperties: "MaterialWarehouse");*/
            var products = await _database.Products.Include(p => p.ProductMaterials).ThenInclude(d => d.MaterialWarehouse).ToListAsync();
            /*var productmaterials = await _database.ProductMaterials.Join(_database.Products, m => m.ProductId, d => d.Id,
                (productmaterial, product) => new
                {
                    Id = product.Id,
                    Name = product.Name,
                    OrderId = product.OrderId,
                    Photo = product.Photo,
                    Link = product.Link,
                    Code = product.Code,
                    Category = product.Category,
                    LengthWithoutPackaging = product.LengthWithoutPackaging,
                    WidthWithoutPackaging = product.WidthWithoutPackaging,
                    HeightWithoutPackaging = product.HeightWithoutPackaging,
                    LengthWithPackaging = product.LengthWithPackaging,
                    WidthWithPackaging = product.WidthWithPackaging,
                    HeightWithPackaging = product.HeightWithPackaging,
                    WeightGross = product.WeightGross,
                    WeightNetto = product.WeightNetto,
                    CollectionTime = product.CollectionTime,
                    BondingTime = product.BondingTime,
                    PaintingTime = product.PaintingTime,
                    LaserTime = product.LaserTime,
                    MilingTime = product.MilingTime,
                    PackagingBoxCode = product.PackagingBoxCode,
                    PackingTime = product.PackingTime,

                    MaterialWarehouseId = productmaterial.MaterialWarehouseId
                }
                ).Join(_database.MaterialsWarehouse, p => p.MaterialWarehouseId, k => k.Id,
                        (product, materialWarehouse) => new
                        {
                            materailTitle = materialWarehouse.Title,
                            materailQuantity = materialWarehouse.Quantity,
                            Id = product.Id,
                            Name = product.Name,
                            OrderId = product.OrderId,
                            Photo = product.Photo,
                            Link = product.Link,
                            Code = product.Code,
                            Category = product.Category,
                            LengthWithoutPackaging = product.LengthWithoutPackaging,
                            WidthWithoutPackaging = product.WidthWithoutPackaging,
                            HeightWithoutPackaging = product.HeightWithoutPackaging,
                            LengthWithPackaging = product.LengthWithPackaging,
                            WidthWithPackaging = product.WidthWithPackaging,
                            HeightWithPackaging = product.HeightWithPackaging,
                            WeightGross = product.WeightGross,
                            WeightNetto = product.WeightNetto,
                            CollectionTime = product.CollectionTime,
                            BondingTime = product.BondingTime,
                            PaintingTime = product.PaintingTime,
                            LaserTime = product.LaserTime,
                            MilingTime = product.MilingTime,
                            PackagingBoxCode = product.PackagingBoxCode,
                            PackingTime = product.PackingTime,
                        }

                ).ToListAsync();*/
            //var results = _mapper.Map<IList<ProductDTO>>(productmaterials);
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


        [HttpPost]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateProduct)}");
                return BadRequest("Submited invalid data");
            }
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
