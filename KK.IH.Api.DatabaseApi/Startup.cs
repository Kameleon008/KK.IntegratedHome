namespace KK.IH.Api.DatabaseApi
{
    using MediatR;
    using KK.IH.Api.DatabaseApi.Consts;
    using KK.IH.Database.PostgresMigration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerDocument();
            services.AddDbContextPool<DatabaseContext>((options) =>
            {
                options.UseNpgsql(this.Configuration.GetSection($"{Appsettings.PostgresClient}:{Appsettings.ConnectionString}").Get<string>());
            });
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
