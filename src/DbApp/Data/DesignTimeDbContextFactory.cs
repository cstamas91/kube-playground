using DbApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DbApp;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DbAppContext>
{
    public DbAppContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<DbAppContext>()
            .UseNpgsql(Environment.GetEnvironmentVariable("DBAPPCONTEXT_CONNECTIONSTRING"))
            .Options;

        return new DbAppContext(options);
    }
}
