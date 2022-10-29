namespace KK.IH.Source.EventHub.Modules
{
    using Autofac;
    using Microsoft.Extensions.Hosting;

    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<OrchestratorHost>().As<IHostedService>().SingleInstance();
        }
    }
}
