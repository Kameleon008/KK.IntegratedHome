using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace KK.IH.Tools.Database.PostgresClient
{
    internal class DiogelContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder =
                new();

            dbContextOptionsBuilder.UseNpgsql(@"Server=localhost;Database=postgres;Port=5432;User Id=postgres;Password=postgres;Ssl Mode=Prefer;Pooling=True;");
            return new DatabaseContext(dbContextOptionsBuilder.Options);
        }
    }
}