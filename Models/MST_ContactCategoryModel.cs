using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace WebAppTABLE.Models
{
    public class MST_ContactCategoryModel
    {

        public int ContactCategoryId { get; set; }

        [Required(ErrorMessage ="Please Enter valid Category Name")]
        [StringLength(20)]
        public string ContactCategoryName { get; set; }

        [Required]
        public string ContactCategoryCode { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
    public class MST_ContactCategoryDropDownModel
    {
        public int? ContactCategoryId { get; set; }
        public string? ContactCategoryName { get; set; }
    }
}
