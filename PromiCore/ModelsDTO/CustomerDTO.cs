using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiCore.ModelsDTO
{
    public class CreateCustomerDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
    }

    public class UpdateCustomerDTO : CreateCustomerDTO
    {

    }
    public class CustomerDTO : CreateCustomerDTO
    {
        public int Id { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }

    }
}
