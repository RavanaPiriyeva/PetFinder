using System;

namespace PetFinder.Models
{
    public class Breed
    {
        public int Id { get; set; }
        public int AnimalKindId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public AnimalKind AnimalKind { get; set; }
    }
}
