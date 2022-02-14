using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiData.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        /*  [ForeignKey(nameof(Order))]
          public int OrderId { get; set; }
          public virtual Order Order { get; set; }*/
        //for photo
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        /*[NotMapped]
        public IFormFile File { get; set; }*/
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
        public string PackagingBoxCode { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<RecentWork> RecentWorks { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; }

        /*public virtual ICollection<ProductService> ProductServices { get; set; }*/
    }
}
