namespace KK.IH.Database.PostgresMigration
{
    using KK.IH.Api.DatabaseApi.Consts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    internal class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        private readonly IConfiguration configuration;

        public DatabaseContextFactory()
        {
            configuration = ConfigureDatabaseContextFactory();
        }

        public DatabaseContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            dbContextOptionsBuilder.UseNpgsql(configuration.GetSection($"{DatabaseConfiguration.PostgresClient}:{DatabaseConfiguration.ConnectionString}").Get<string>());
            return new DatabaseContext(dbContextOptionsBuilder.Options);
        }

        private IConfiguration ConfigureDatabaseContextFactory()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile($"appsettings.json", optional: false);
            configBuilder.AddUserSecrets<DatabaseContextFactory>();
            return configBuilder.Build();
        }
    }
}