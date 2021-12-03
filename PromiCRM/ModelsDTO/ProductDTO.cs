using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateProductDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string Photo { get; set; }
        public string Link { get; set; }
        [Required]
        public string Code { get; set; }
        public string Category { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double LengthWithoutPackaging { get; set; }
        [Required]
        public double WidthWithoutPackaging { get; set; }
        [Required]
        public double HeightWithoutPackaging { get; set; }
        [Required]
        public double LengthWithPackaging { get; set; }
        [Required]
        public double WidthWithPackaging { get; set; }
        [Required]
        public double HeightWithPackaging { get; set; }
        [Required]
        public double WeightGross { get; set; }
        [Required]
        public double WeightNetto { get; set; }
        [Required]
        public int? CollectionTime { get; set; }
        [Required]
        public int? BondingTime { get; set; }
        [Required]
        public int? PaintingTime { get; set; }
        [Required]
        public int? LaserTime { get; set; }
        [Required]
        public int? MilingTime { get; set; }
        [Required]
        public string PackagingBoxCode { get; set; }
        [Required]
        public double PackingTime { get; set; }
       
        public IList<ProductMaterialDTO> ProductMaterials { get; set; }
    }

    public class UpdateProductDTO : CreateProductDTO
    {

    }
    public class ProductDTO : CreateProductDTO
    {
        public int Id { get; set; }
        public OrderDTO Order { get; set; }
       /* public ServiceDTO Service { get; set; }*/
        //public virtual IList<ProductMaterialDTO> ProductMaterials { get; set; }

    }
}
