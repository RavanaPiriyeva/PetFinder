using System.ComponentModel.DataAnnotations;

namespace PetFinder.Areas.Manage.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [MaxLength(25)]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
