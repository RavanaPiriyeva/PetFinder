using System.ComponentModel.DataAnnotations;

namespace PetFinder.ViewModels
{
    public class MemberUpdateViewModel
    {
        [Required]
        [StringLength(maximumLength: 20)]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 25, MinimumLength = 8)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        [StringLength(maximumLength: 25, MinimumLength = 8)]
        public string RepeatPassword { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 8)]
        public string CurrentPassword { get; set; }
    }
}
