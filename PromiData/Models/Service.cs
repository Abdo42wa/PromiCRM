using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiData.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; }
    }
}
