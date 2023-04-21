using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace WebAppTABLE.Models
{
    public class CON_ContactModel
    {
        [Required]
       
        public int ContactId { get; set; }

        [Required(ErrorMessage ="Insert validate name")]
        
        public string PersonName { get; set; }
        public string MobileNumber { get; set; }

        public string Address { get; set; }


        public string Email { get; set; }

        public string Gender { get; set; }
        public string? AlternateMobileNumber { get; set; }


        public string? Instagram { get; set; }


        public string? LinkedIn { get; set; }

        public string? Twitter { get; set; }

        public int ContactCategoryId { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
