using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MaterialUsed { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [NotMapped]
        public Product Product { get; set; }
        //public virtual IList<NonStandardWorks> NonStandardWorks { get; set; }
        
    }
}
