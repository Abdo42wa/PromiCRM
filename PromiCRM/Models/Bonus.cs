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
        public Guid UserId { get; set; }
        [NotMapped]
        public ApiUser ApiUser { get; set; }
        public int Quantity { get; set; }
        public int Accumulated { get; set; }
        public int Bonusas { get; set; }
        public int LeftUntil { get; set; }
    }
}
