using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KK.IH.Source.EventHub
{
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
                    configBuilder.AddEnvironmentVariables();
                })
                .ConfigureLogging((context, loggingBuilder) =>
                {

                })
                .ConfigureContainer<ContainerBuilder>((hostBuilder, containerBuilder) =>
                {
                    containerBuilder.RegisterAssemblyModules(typeof(Program).Assembly);
                })
                .UseConsoleLifetime();
        }

    }
}
