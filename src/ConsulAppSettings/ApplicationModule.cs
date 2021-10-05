using Autofac;
using Microsoft.Extensions.Configuration;

namespace ConsulAppSettings
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Application>();
            builder.Register(context => ApplicationSettings.Create(context.Resolve<IConfiguration>()));
        }
    }
}