using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Configurations;
using Model.Entities;
using Model.Identity;

namespace Model
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        protected readonly IConfiguration Configuration;

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
              /* optionsBuilder.UseSqlServer("Server=LAPTOP-O3PUJRAM;Database=TNAI;Trusted_Connection=True;MultipleActiveResultSets=True"); */
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("AppConnection"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
