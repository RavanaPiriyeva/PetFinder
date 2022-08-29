using System;
using System.ComponentModel.DataAnnotations;

namespace PetFinder.Models
{
    public class BlogComment
    {
        public int  Id  { get; set; }
        public int BlogId { get; set; }
        public string AppUserId { get; set; }
        [StringLength(maximumLength: 500)]
        public string Text { get; set; }
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
        [StringLength(maximumLength: 50)]
        public string FullName { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Blog Blog { get; set; }
        public AppUser AppUser { get; set; }
    }
}
