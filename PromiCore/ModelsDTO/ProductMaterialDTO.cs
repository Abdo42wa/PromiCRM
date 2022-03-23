using System;
using System.ComponentModel.DataAnnotations;

namespace PromiCore.ModelsDTO
{
    public class CreateProductMaterialDTO
    {
        [Required]
        public int Quantity { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public DateTime RegisterDate { get; set; }
        [Required]
        public int MaterialWarehouseId { get; set; }
    }
    public class UpdateProductMaterialDTO : CreateProductMaterialDTO
    {
        public int Id { get; set; }
    }

    public class ProductMaterialDTO : CreateProductMaterialDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public OrderDTO Order { get; set; }
        public MaterialWarehouseDTO MaterialWarehouse { get; set; }

    }
}
