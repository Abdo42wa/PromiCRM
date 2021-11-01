using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Data
{
    public class Non_standardWorks
    {
        public int Id { get; set; }

        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime OrderDeadline { get; set; }
        public int DaysUntilDeadline { get; set; }

        [ForeignKey(nameof(Customer))]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public string Device { get; set; }
        public int PlannedProductionTime { get; set; }
        public string Comment { get; set; }

        [ForeignKey(nameof(Material))]
        public Material Material { get; set; }
        public int MaterialId { get; set; }

        public bool Status { get; set; }
    }
}
