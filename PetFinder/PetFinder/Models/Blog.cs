using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Models
{
    public class Blog
    { 
        public int Id { get; set; } 
        public int ShelterId { get; set; }
        public int AnimalKindId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Shelter Shelter { get; set; }
        public AnimalKind AnimalKind { get; set; }

        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<BlogCategory> BlogCategories { get; set; }
        [NotMapped]
        public List<int> CategoryId { get; set; } = new List<int>();

        public List<BlogComment> BlogComments { get; set; }
    }
}
