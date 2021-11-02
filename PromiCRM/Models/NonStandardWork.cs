﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class NonStandardWork
    {
        [Key]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime OrderDeadline { get; set; }
        public int DaysUntilDeadline { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        [NotMapped]
        public Customer Customer { get; set; }
        public string Device { get; set; }
        public int PlannedProductionTime { get; set; }
        public string Comment { get; set; }

        [ForeignKey(nameof(Material))]
        public int MaterialId { get; set; }
        [NotMapped]
        public Material Material { get; set; }
        public bool Status { get; set; }
    }
}