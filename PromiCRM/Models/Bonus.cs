using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Bonus
    {
        public int Id { get; set; }
        [ForeignKey(nameof(ApiUser))]
        public int UserId { get; set; }
        public ApiUser ApiUser { get; set; }
        public int Quantity { get; set; }
        public int Accumulated { get; set; }
        public int Bonuas { get; set; }
        public int LeftUntil { get; set; }
    }
}
