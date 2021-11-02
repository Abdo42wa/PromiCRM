using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateProductDTO
    {
        /* [ForeignKey(nameof(Order))]
         public Order Order { get; set; }
         public int OrderId { get; set; }*/
        [Required]
        public string Photo { get; set; }
        public string Link { get; set; }
        [Required]
        public string code { get; set; }
        public string Category { get; set; }
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "The Product  Name cannot be more than 100 or less than 10 ")]
        public string Name { get; set; }
        [Required]
        public double LengthWithoutPackaging { get; set; }
        [Required]
        public double WidthWithoutPackaging { get; set; }
        [Required]
        public double HeightWithoutPackaging { get; set; }
        [Required]
        public double WeightGross { get; set; }
        [Required]
        public string PackagingBoxCode { get; set; }
        [Required]
        public double PackingTime { get; set; }

     /*   [ForeignKey(nameof(Services))]
        public Services Services { get; set; }
        public int ServiceId { get; set; }*/
    }

    public class UpdateProductDTO : CreateProductDTO
    {

    }
    public class ProductDTO
    {
        public int Id { get; set; }
        public virtual IList<OrderDTO> Order { get; set; }
        public virtual IList<ServiceDTO> Services { get; set; }


    }
}
