using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWheels.Entities;

namespace NWheels.Domains.ECommerce
{
    public interface IECommerceDomainContext : IApplicationDataRepository
    {
        IEntityRepository<IAttributeEntity> Attributes { get; }

        IAttributeEntity NewAttribute(string name);
        IAttributeValueEntity NewAttributeValue(string name);
    }
}
