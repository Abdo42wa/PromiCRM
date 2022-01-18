using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateWeeklyWorkScheduleDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string DarbasApibūdinimas { get; set; }

        [Required]
        public bool Atlikta { get; set; }
        public DateTime Date { get; set; }

    }
    public class UpdateWeeklyWorkScheduleDTO : CreateWeeklyWorkScheduleDTO
    {
    }
    public class WeeklyWorkScheduleDTO : CreateWeeklyWorkScheduleDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
    }
}
