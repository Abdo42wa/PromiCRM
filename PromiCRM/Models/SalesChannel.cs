using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class SalesChannel
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
        public double Discount { get; set; }
        public double BrokerageFee { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
