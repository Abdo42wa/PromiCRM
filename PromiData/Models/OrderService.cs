using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromiData.Models
{
    public class OrderService
    {
        [Key]
        public int Id { get; set; }

       /* [ForeignKey(nameof(Product))]
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }*/

        [ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        //and service id(laseriavimas,frezavimas ...)
        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public int TimeConsumption { get; set; }
        public virtual ICollection<UserService> UserServices { get; set; }
    }
}
