using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace WebAppTABLE.Models
{
    public class LOC_CountryModel
    {

        public int? CountryId { get; set; }

        [Required(ErrorMessage ="Please enter valid name")]
        public string CountryName { get; set; }

        [Required]
        public string CountryCode { get; set; }

        public IFormFile? File { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
    public class LOC_CountryDropDownModel
    {
        public int? CountryId { get; set; }

        public string? CountryName { get; set; }
    }
}