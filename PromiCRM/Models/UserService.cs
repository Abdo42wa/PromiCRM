using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class UserService
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(OrderService))]
        public int OrderServiceId { get; set; }
        public virtual OrderService OrderService { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
