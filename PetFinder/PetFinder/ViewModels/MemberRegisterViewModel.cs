using System.ComponentModel.DataAnnotations;

namespace PetFinder.ViewModels
{
    public class MemberRegisterViewModel
    {
        [Required]
        [MaxLength(25)]
        [MinLength(6)]
        public string RegisterUserName { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(4)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string RegisterPassword { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(RegisterPassword))]
        public string ConfirmPassword { get; set; }
    }
}
