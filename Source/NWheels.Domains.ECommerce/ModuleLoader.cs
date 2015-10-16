using Autofac;
using NWheels.Extensions;

namespace NWheels.Domains.ECommerce
{
    public class ModuleLoader : Module
    {
        #region Overrides of Module

        protected override void Load(ContainerBuilder builder)
        {
            builder.NWheelsFeatures().Entities().RegisterDataRepository<IECommerceDomainContext>()
                .WithInitializeStorageOnStartup()
                .WithRestEndpoint(defaultListenUrl: "http://localhost:8901/rest");
        }

        #endregion
    }
}
