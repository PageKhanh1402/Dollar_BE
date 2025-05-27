using System.ComponentModel.DataAnnotations;

namespace DollarProject.ViewModels
{
    public class ProfileViewModel
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string SellerDescription { get; set; }

        public string? ImageURL { get; set; }

        [Display(Name = "Profile Photo")]
        public IFormFile? ProfilePhoto { get; set; } 
    }


}
