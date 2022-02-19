using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiCore.ModelsDTO
{

    public class CreateBonusDTO
    {
        public int Quantity { get; set; }
        public int IndividualBonusQuantity { get; set; }
        //bascially particular month that bonus will apply to
        public DateTime Date { get; set; }
        //how many bonuses have in this month. maybe goal was 1000 but team made 2000. then accumulated will be 2
        public int Accumulated { get; set; }
        public int Reward { get; set; }
    }
    public class UpdateBonusDTO : CreateBonusDTO
    {

    }
    public class BonusDTO : CreateBonusDTO
    {
        public int Id { get; set; }
        public IList<UserBonusDTO> UserBonuses { get; set; }
    }
}
