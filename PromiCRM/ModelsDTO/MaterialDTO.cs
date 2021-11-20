using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateMaterialDTO
    {
        [Required]
        public string Name { get; set; }

        public string MaterialUsed { get; set; }
        public int ProductId { get; set; }
    }
    public class UpdateMaterialDTO : CreateMaterialDTO
    {

    }

    public class MaterialDTO : CreateMaterialDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        
    }
}
