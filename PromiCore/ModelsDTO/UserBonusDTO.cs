using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromiCore.ModelsDTO
{
    public class CreateUserBonusDTO
    {
        public Guid UserId { get; set; }
        public int? BonusId { get; set; }
        public DateTime Date { get; set; }
        public int Reward { get; set; }
    }
    public class UpdateUserBonusDTO : CreateUserBonusDTO
    {
    }
    public class UserBonusDTO : CreateUserBonusDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public BonusDTO Bonus { get; set; }
    }
}
