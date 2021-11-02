using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateServiceDTO
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "The Service  Name cannot be more than 100 or less than 10 ")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "The Service  Time cannot be more than 50 or less than 3 ")]
        public string Time { get; set; }
    }
    public class UpdateServiceDTO : CreateServiceDTO
    {

    }
    public class ServiceDTO
    {
        public int Id { get; set; }
    }
}
