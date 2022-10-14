namespace KK.IH.Tools.Database.PostgresClient
{
    using KK.IH.Api.DatabaseApi;
    using KK.IH.Api.DatabaseApi.Consts;
    using KK.IH.Api.DatabaseApi.Database;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    internal class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        private readonly IConfiguration configuration;

        public DatabaseContextFactory()
        {
            this.configuration = this.ConfigureDatabaseContextFactory();
        }

        public DatabaseContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            dbContextOptionsBuilder.UseNpgsql(this.configuration.GetSection($"{Appsettings.PostgresClient}:{Appsettings.ConnectionString}").Get<string>());
            return new DatabaseContext(dbContextOptionsBuilder.Options);
        }

        private IConfiguration ConfigureDatabaseContextFactory()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile($"appsettings.json", optional: false);
            configBuilder.AddUserSecrets<Program>();
            return configBuilder.Build();
        }
    }
}