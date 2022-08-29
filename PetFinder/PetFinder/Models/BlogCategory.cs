namespace PetFinder.Models
{
    public class BlogCategory
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
       public int CatagoryId { get; set; }

        public Blog Blog { get; set; }
       public Category Catagory { get; set; }
    }
}
