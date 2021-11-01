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
        [StringLength(maximumLength: 100, MinimumLength =3, ErrorMessage = "The Country  Name cannot be more than 100 or less than 3 ")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 2, ErrorMessage = "The Country Short Name cannot be more than 3 or less than 2 ")]
        public string ShortName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "The Continent  Name cannot be more than 3 or less than 2 ")]
        public string continent { get; set; }
    }
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
    }
}
