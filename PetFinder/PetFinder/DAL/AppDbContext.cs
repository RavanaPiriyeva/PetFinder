using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetFinder.Models;

namespace PetFinder.DAL
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<AnimalKind> AnimalKinds { get; set; }
        public DbSet<Breed>Breeds { get; set; }
        public DbSet<Color>Colors { get; set; }
        public DbSet<AnimalImage> AnimalImages { get; set; }
        public DbSet<Animal> Animals    { get; set; }
        public DbSet<AppUser> Users { get; set; }

       
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
