using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWheels.Domains.ECommerce.Tests.App.Deployment;
using NWheels.Domains.ECommerce.Tests.App.Domain;
using NWheels.Domains.ECommerce.Tests.App.UI;
using NWheels.Domains.Security;
using NWheels.Extensions;

namespace NWheels.Domains.ECommerce.Tests.App
{
    public class ModuleLoader : Module
    {
        #region Overrides of Module

        protected override void Load(ContainerBuilder builder)
        {
            builder.NWheelsFeatures().ObjectContracts().Concretize<IUserAccountDataRepository>().With<IECommerceAppContext>();
            builder.NWheelsFeatures().ObjectContracts().Concretize<IECommerceDomainContext>().With<IECommerceAppContext>();

            builder.NWheelsFeatures().Entities().RegisterDataPopulator<MinimalDataPopulator>();

            builder.NWheelsFeatures().UI().RegisterApplication<BackOfficeApp>()
                .WithWebEndpoint(defaultUrl: "http://localhost:8901/admin");
        }

        #endregion
    }
}
