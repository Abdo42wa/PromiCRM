using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }

        //relationship one to many. service can have many products
        public virtual IList<Product> Products { get; set; }
    }
}
