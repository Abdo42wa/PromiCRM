using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiCore.ModelsDTO
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
    //for register we need all fields
    public class UserDTO : LoginUserDTO
    {
        [Required]
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int TypeId { get; set; }
        public UserTypeDTO UserType { get; set; }
        public byte[] UserPhoto { get; set; }
        public IList<BonusDTO> Bonus { get; set; }
        public IList<OrderDTO> Orders { get; set; }
        public IList<WeeklyWorkScheduleDTO> WeeklyWorkSchedules { get; set; }
        public IList<RecentWorkDTO> RecentWorks { get; set; }
        public IList<UserServiceDTO> UserServices { get; set; }
    }

    public class DisplayUserDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int TypeId { get; set; }
        public UserTypeDTO UserType { get; set; }
        public byte[] UserPhoto { get; set; }
        public virtual IList<BonusDTO> Bonus { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
        public virtual IList<WeeklyWorkScheduleDTO> WeeklyWorkSchedules { get; set; }
        public virtual IList<RecentWorkDTO> RecentWorks { get; set; }
    }
}
