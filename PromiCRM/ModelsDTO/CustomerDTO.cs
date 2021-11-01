using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateCustomerDTO
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "The Customer  Name cannot be more than 50 or less than 3 ")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "The Customer Last  Name cannot be more than 50 or less than 3 ")]
        public string LastName { get; set; }
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 20, ErrorMessage = "The Customer  Email cannot be more than 100 or less than 20 ")]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 16, MinimumLength = 9, ErrorMessage = "The Customer  Phone Number cannot be more than 16 or less than 9 ")]
        public string PhoneNumber { get; set; }
        
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "The Customer  Company Name cannot be more than 100 or less than 5 ")]
        public string CompanyName { get; set; }
    }
    public class CustomerDTO : CreateCustomerDTO
    {
        public int Id { get; set; }
    }
}
