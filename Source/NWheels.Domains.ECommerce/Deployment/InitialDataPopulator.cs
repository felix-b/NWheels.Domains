using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWheels.Entities;

namespace NWheels.Domains.ECommerce.Deployment
{
    public class InitialDataPopulator : IDataRepositoryPopulator
    {
        private readonly IFramework _framework;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public InitialDataPopulator(IFramework framework)
        {
            _framework = framework;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        #region Implementation of IDataRepositoryPopulator

        public void Populate()
        {
            using ( var context = _framework.NewUnitOfWork<IECommerceDomainContext>() )
            {
                context.CommitChanges();      
            }
        }

        #endregion
    }
}
