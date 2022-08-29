using System;

namespace PetFinder.Models
{
    public class Shelter
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Adrress { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public City City { get; set; }

    }
}
