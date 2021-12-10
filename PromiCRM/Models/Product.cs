using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        //for photo
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string Link { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public double LengthWithoutPackaging { get; set; }
        public double WidthWithoutPackaging { get; set; }
        public double HeightWithoutPackaging { get; set; }
        public double LengthWithPackaging { get; set; }
        public double WidthWithPackaging { get; set; }
        public double HeightWithPackaging { get; set; }
        public double WeightGross { get; set; }
        public double WeightNetto { get; set; }
        public int? CollectionTime { get; set; }
        public int? BondingTime { get; set; }
        public int? PaintingTime { get; set; }
        public int? LaserTime { get; set; }
        public int? MilingTime { get; set; }
        public string PackagingBoxCode { get; set; }
        public double PackingTime { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}
