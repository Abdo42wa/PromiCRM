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
        public string Photo { get; set; }
        public string Link { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public double LengthWithoutPackaging { get; set; }
        public double WidthWithoutPackaging { get; set; }
        public double HeightWithoutPackaging { get; set; }
        public double WeightGross { get; set; }
        public string PackagingBoxCode { get; set; }
        public double PackingTime { get; set; }
        [ForeignKey(nameof(Services))]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        //relationship one to many with Material
        public virtual ICollection<ProductMaterial> ProducMaterials { get; set; }
    }
}
