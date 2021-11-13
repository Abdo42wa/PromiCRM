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
        [StringLength(maximumLength: 700, MinimumLength = 10, ErrorMessage = "The Material  Name cannot be more than 70 or less than 10 ")]
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
        public virtual IList<NonStandardWorkDTO> NonStandardWorks { get; set; }
    }
}
