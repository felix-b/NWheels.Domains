using NWheels.Domains.Security;

namespace NWheels.Domains.ECommerce.Tests.App.Domain
{
    public interface IECommerceAppContext : IUserAccountDataRepository, IECommerceDomainContext
    {
    }
}
