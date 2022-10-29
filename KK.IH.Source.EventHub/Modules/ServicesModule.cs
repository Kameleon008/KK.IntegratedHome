namespace KK.IH.Source.EventHub.Modules
{
    using Autofac;
    using KK.IH.Source.EventHub.Components.EventProcessing;
    using Microsoft.Extensions.Hosting;

    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<OrchestratorHost>().As<IHostedService>().SingleInstance();
            builder.RegisterType<EventProcessor>().As<IEventProcessor>().SingleInstance();
        }
    }
}
