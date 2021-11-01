using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class NonStandardWorks
    {
        public int Id { get; set; }

        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime OrderDeadline { get; set; }
        public int DaysUntilDeadline { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public string Device { get; set; }
        public int PlannedProductionTime { get; set; }
        public string Comment { get; set; }

        [ForeignKey(nameof(Material))]
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        

        public bool Status { get; set; }
    }
}
