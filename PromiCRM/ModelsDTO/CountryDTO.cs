using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateCountryDTO
    {
        [Required]
        [StringLength(maximumLength: 35, MinimumLength =  1, ErrorMessage = "The Country  Name cannot be more than 100 or less than 3 ")]
        public string Name { get; set; }
        [StringLength(maximumLength: 5, MinimumLength = 1, ErrorMessage = "The Country Short Name cannot be more than 1 or less than 5 ")]
        public string ShortName { get; set; }
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "The Continent  Name cannot be more than 3 or less than 2 ")]
        public string Continent { get; set; }
    }

    public class UpdateCountryDTO
    {

    }
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
    }
}
