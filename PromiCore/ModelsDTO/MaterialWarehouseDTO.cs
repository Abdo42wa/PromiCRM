using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiCore.ModelsDTO
{
    public class CreateMaterialWarehouseDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string MeasuringUnit { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Info { get; set; }
        [Required]
        public int DeliveryTime { get; set; }
        [Required]
        public int UseDays { get; set; }
        [Required]
        public DateTime LastAdittion { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
    }
    public class UpdateMaterialWarehouseDTO : CreateMaterialWarehouseDTO
    {

    }
    public class MaterialWarehouseDTO : CreateMaterialWarehouseDTO
    {
        public int Id { get; set; }
        public IList<ProductMaterialDTO> ProductMaterials { get; set; }
    }
}
