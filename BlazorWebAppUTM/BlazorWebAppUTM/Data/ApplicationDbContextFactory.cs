using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlazorWebAppUTM.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        // Try multiple connection strings
        var connectionStrings = new[]
        {
            "Server=.\\SQLEXPRESS;Database=PracticaDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true",
            "Server=(local)\\SQLEXPRESS;Database=PracticaDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true",
            "Server=localhost\\SQLEXPRESS;Database=PracticaDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true",
            "Server=(localdb)\\MSSQLLocalDB;Database=PracticaDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
        };

        // Use the first connection string for design time
        optionsBuilder.UseSqlServer(connectionStrings[0]);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}