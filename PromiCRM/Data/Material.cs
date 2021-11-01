﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Data
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MaterialUsed { get; set; }

        [ForeignKey(nameof(Product))]
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
