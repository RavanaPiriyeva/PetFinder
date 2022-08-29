namespace PetFinder.Models
{
    public class AnimalImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AnimalId { get; set; }
        public bool? PosterStatus { get; set; }

        public Animal Animal { get; set; }
    }
}
