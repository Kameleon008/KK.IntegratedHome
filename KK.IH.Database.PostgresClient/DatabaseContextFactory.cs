namespace KK.IH.Tools.Database.PostgresClient
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

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