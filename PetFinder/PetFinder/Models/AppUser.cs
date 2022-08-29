using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace PetFinder.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(maximumLength: 50)]
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime LastConnectDate { get; set; }
        [StringLength(maximumLength: 50)]
        public string ConnectionId { get; set; }
    }
}
