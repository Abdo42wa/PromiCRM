using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class WeeklyWorkSchedule
    {
        public int id { get; set; }

        // we need to get the user name 
        [ForeignKey(nameof(ApiUser))]
        public Guid UserId { get; set; }
        [NotMapped]
        public ApiUser ApiUser { get; set; }
        
        public string DarbasApibūdinimas { get; set; }
        public bool Atlikta { get; set; }
    }
}
