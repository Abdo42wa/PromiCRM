using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class RecentWork
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        public int? Quantity { get; set; }
        public string WorkTitle { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public DateTime? Time { get; set; }
    }
}
