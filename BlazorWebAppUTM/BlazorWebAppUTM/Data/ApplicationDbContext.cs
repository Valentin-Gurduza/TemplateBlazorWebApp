using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAppUTM.Data
{
    // Use IdentityUserContext to include only the AspNetUsers table
    public class ApplicationDbContext : IdentityUserContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Rename AspNetUsers table to UserCreation
            builder.Entity<ApplicationUser>().ToTable("UserCreation");
        }
    }
}
