using System;
using System.ComponentModel.DataAnnotations;

namespace PromiCore.ModelsDTO
{

    public class CreateBonusDTO
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int Accumulated { get; set; }
        [Required]
        public int Bonusas { get; set; }
        /*        [Required]
                public int LeftUntil { get; set; }*/
    }
    public class UpdateBonusDTO : CreateBonusDTO
    {

    }
    public class BonusDTO : CreateBonusDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
    }
}
