using backend.Api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Api.data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<UserMovie> UserMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Composite key: UserId + MovieId
            builder.Entity<UserMovie>()
                .HasKey(um => new { um.UserId, um.MovieId });

            builder.Entity<UserMovie>()
                .HasOne(um => um.User)
                .WithMany()
                .HasForeignKey(um => um.UserId);

            builder.Entity<UserMovie>()
                .HasOne(um => um.Movie)
                .WithMany()
                .HasForeignKey(um => um.MovieId);
        }
    }
}
