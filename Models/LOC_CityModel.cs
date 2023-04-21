using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace WebAppTABLE.Models
{
    public class LOC_CityModel
    {
        
        public int CityId { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public string CityCode { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }

        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
    public class LOC_CityDropDownModel
    {
        public int? CityId { get; set; }
        public string CityName { get; set; }
    }
}
