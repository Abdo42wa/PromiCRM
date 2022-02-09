using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiCore.ModelsDTO
{
    public class CreateServiceDTO
    {
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "The Service  Name cannot be more than 40 or less than 5 ")]
        public string Name { get; set; }
    }
    public class UpdateServiceDTO : CreateServiceDTO
    {

    }
    public class ServiceDTO : CreateServiceDTO
    {
        public int Id { get; set; }
        public IList<OrderServiceDTO> OrderServices { get; set; }
    }
}
