using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateSalesChannelDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double BrokerageFee { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
    public class UpdateSalesChannelDTO : CreateSalesChannelDTO
    {
    }

    public class SalesChannelDTO : CreateSalesChannelDTO
    {
        public Guid Id { get; set; }
        public UserDTO User { get; set; }
    }
}
