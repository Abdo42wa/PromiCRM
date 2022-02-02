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
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
        public double Discount { get; set; }
        public double BrokerageFee { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
    public class UpdateSalesChannelDTO : CreateSalesChannelDTO
    {
    }

    public class SalesChannelDTO : CreateSalesChannelDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
    }
}
