using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateNonStandardWorksDTO
    {

        [Required]
        public int OrderNumber { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime OrderDeadline { get; set; }
        [Required]
        public int DaysUntilDeadline { get; set; }

        /* [ForeignKey(nameof(Customer))]
         public Customer Customer { get; set; }
         public int CustomerId { get; set; }*/
        [Required]
        public string Device { get; set; }
        [Required]
        public int PlannedProductionTime { get; set; }
        public string Comment { get; set; }
        /*
        [ForeignKey(nameof(Material))]
        public Material Material { get; set; }
        public int MaterialId { get; set; }*/

        [Required]
        public bool Status { get; set; }
    }

    public class UpdateNonStandardWorksDTO : CreateNonStandardWorksDTO
    {

    }

    public class NonStandardWorkDTO : CreateNonStandardWorksDTO
    {
        public int Id { get; set; }
        public CustomerDTO Customer { get; set; }
        public MaterialDTO Material { get; set; }
    }
}
