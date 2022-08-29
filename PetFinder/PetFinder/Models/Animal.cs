using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public int BreedId  { get; set; }
        public int ShelterId  { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }

        public Color Color { get; set; }
        public Breed Breed { get; set; }
        public Shelter Shelter { get; set; }

        public List<AnimalImage> BookImages { get; set; } = new List<AnimalImage>();

        [NotMapped]
        public IFormFile PosterFile { get; set; }

        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }

        [NotMapped]
        public List<int> ImageIds { get; set; }


    }
}
