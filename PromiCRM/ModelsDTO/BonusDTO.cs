using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{

    public class CreateBonusDTO
    {

        [Required]
        [Range(100, 10000,ErrorMessage ="Your maxima is 10000")]
        public int Quantity { get; set; }
        
        public int Accumulated { get; set; }
        [Required]
        [Range(100, 1000, ErrorMessage = "Your maxima is 1000")]
        public int Bonuas { get; set; }
        [Required]
        [Range(1, 365, ErrorMessage = "Your maxima is 365")]
        public int LeftUntil { get; set; }
    }
    public class UpdateBonusDTO : CreateBonusDTO
    {

    }
    public class BonusDTO : CreateBonusDTO
    {
        public int Id { get; set; }
        public virtual IList<UserDTO> Users { get; set; }
    }
}
