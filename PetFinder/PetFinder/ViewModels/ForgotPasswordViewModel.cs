using System.ComponentModel.DataAnnotations;

namespace PetFinder.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
    }
}
