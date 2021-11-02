using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateWeeklyWorkScheduleDTO
    {


        // we need to get the user name 
        /* [ForeignKey(nameof(ApiUser))]
         public ApiUser ApiUser { get; set; }
         public int UserId { get; set; }*/
        [Required]
        public string DarbasApibūdinimas { get; set; }
        [Required]
        public bool Atlikta { get; set; }

    }
    public class UpdateWeeklyWorkScheduleDTO : CreateWeeklyWorkScheduleDTO
    {

    }
    public class WeeklyWorkScheduleDTO
    {
        public int Id { get; set; }
        public UserDTO ApiUser { get; set; }
    }
}
