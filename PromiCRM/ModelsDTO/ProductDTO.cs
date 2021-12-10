using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateProductDTO
    {
        public int OrderId { get; set; }
        //for photo
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
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
