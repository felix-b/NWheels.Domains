using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWheels.Domains.ECommerce.Tests.App.Domain;
using NWheels.Domains.Security.Core;
using NWheels.Entities;
using NWheels.Extensions;
using NWheels.Utilities;

namespace NWheels.Domains.ECommerce.Tests.App.Deployment
{
    public class MinimalDataPopulator : IDataRepositoryPopulator
    {
        private readonly IFramework _framework;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public MinimalDataPopulator(IFramework framework)
        {
            _framework = framework;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        #region Implementation of IDataRepositoryPopulator

        public void Populate()
        {
            using ( var context = _framework.NewUnitOfWork<IECommerceAppContext>() )
            {
                CreateAdministratorAccount(context);
                CreateAttributes(context);

                context.CommitChanges();
            }
        }

        #endregion

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        private void CreateAttributes(IECommerceAppContext context)
        {
            var color = context.NewAttribute("Color");
            
            color.Values.Add(context.NewAttributeValue("Black"));
            color.Values.Add(context.NewAttributeValue("White"));

            context.Attributes.Insert(color);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        private void CreateAdministratorAccount(IECommerceAppContext context)
        {
            var administratorRole = context.UserRoles.New();

            administratorRole.Name = "Administrator";
            administratorRole.ClaimValue = "Administrator";

            var administrator = context.AllUsers.New();

            administrator.FullName = "Administrator";
            administrator.LoginName = "admin";
            administrator.As<UserAccountEntity>().SetPassword(SecureStringUtility.ClearToSecure("1111"));
            administrator.AssociatedRoles = new[] { administratorRole.ClaimValue };

            context.UserRoles.Insert(administratorRole);
            context.AllUsers.Insert(administrator);
        }
    }
}
