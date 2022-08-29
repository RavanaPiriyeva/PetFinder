using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetFinder.Models
{
    public class Category
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "Bu hissəni doldurun!")]
        [MaxLength(50, ErrorMessage = "Uzunluq max 50 ola biler! ")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<BlogCategory> BlogCategories { get; set; }
       
    }
}
