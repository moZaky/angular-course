using AngularDotnet.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngularDotnet.Core
{
    public class MovieCatalogDbContext : DbContext
    {

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public MovieCatalogDbContext(DbContextOptions<MovieCatalogDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
