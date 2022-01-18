using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class WeeklyWorkSchedule
    {
        [Key]
        public int Id { get; set; }
        // we need to get the user name 
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string DarbasApibūdinimas { get; set; }
        public bool Atlikta { get; set; }
        public DateTime Date { get; set; }
    }
}
