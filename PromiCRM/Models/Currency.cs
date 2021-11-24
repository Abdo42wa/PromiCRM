using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //relationship one to many. one currency can have multiple orders.
        public virtual ICollection<Order> Orders { get; set; }
    }
}
