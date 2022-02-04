using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class OrderService
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Product))]
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
        //and service id(laseriavimas,frezavimas ...)
        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        
        public int TimeConsumption { get; set; }
    }
}
