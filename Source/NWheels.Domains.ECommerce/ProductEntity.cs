using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWheels.DataObjects;
using NWheels.Entities;

namespace NWheels.Domains.ECommerce
{
    [EntityContract]
    public interface IProductEntity
    {
        [PropertyContract.EntityId]
        string CatalogNo { get; set; }


    }

    class ProductEntity
    {
    }
}
