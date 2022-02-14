using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromiData.Models
{
    public class MaterialWarehouse
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string MeasuringUnit { get; set; }
        public int Quantity { get; set; }
        public string Info { get; set; }
        public int DeliveryTime { get; set; }
        public int UseDays { get; set; }
        public DateTime LastAdittion { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}
