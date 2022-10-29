namespace KK.IH.Source.EventHub
{
    using System;
    using System.IO;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using KK.IH.Source.EventHub.Components.EventProcessing.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return Host.CreateDefaultBuilder(args)
                .UseEnvironment(env)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    configBuilder.AddJsonFile("appsettings.json", optional: false);
                    configBuilder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
                    configBuilder.AddJsonFile("/mnt/config/config.json", optional: true);
                    configBuilder.AddJsonFile("/mnt/secrets/secrets.json", optional: true);
                    configBuilder.AddUserSecrets(typeof(Program).Assembly);
                    configBuilder.AddEnvironmentVariables();
                })
                .ConfigureLogging((context, loggingBuilder) =>
                {

                })
                .ConfigureContainer<ContainerBuilder>((hostBuilder, containerBuilder) =>
                {
                    containerBuilder.RegisterInstance(hostBuilder.Configuration.GetSection($"{ConfigKey.EventProcessorConfiguration}").Get<EventProcessorConfiguration>()).As<IEventProcessorConfiguration>();
                    containerBuilder.RegisterAssemblyModules(typeof(Program).Assembly);
                })
                .UseConsoleLifetime();
        }

    }
}
