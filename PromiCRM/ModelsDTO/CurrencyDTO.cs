using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateCurrencyDTO
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "The Currency  Name cannot be more than 30 or less than 3 ")]
        public string Name { get; set; }

    }
    public class CurrencyDTO : CreateCurrencyDTO
    {
        public int Id { get; set; }
    }
}
