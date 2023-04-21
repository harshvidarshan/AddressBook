using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace WebAppTABLE.Models
{
    public class LOC_StateModel
    {
        public int StateId { get; set; }
        public int CountryId { get; set; }

        [Required(ErrorMessage ="Enter valid name")]
        public string StateName { get; set; }
        [Required]
        public string StateCode { get; set; }

        public IFormFile File { get; set; }
        public string? PhotoPath { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
    public class LOC_StateDropDownModel
    {

        public int? StateId { get; set; }

        public string? StateName { get; set; }

    }
}
