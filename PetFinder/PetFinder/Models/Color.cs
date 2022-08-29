using System;
using System.ComponentModel.DataAnnotations;

namespace PetFinder.Models
{
    public class Color
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Bu hissəni doldurun!")]
        [MaxLength(20, ErrorMessage = "Uzunluq max 20 ola biler! ")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
