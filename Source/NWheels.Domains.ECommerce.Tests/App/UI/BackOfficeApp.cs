using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWheels.Domains.Security;
using NWheels.Domains.Security.UI;
using NWheels.Globalization;
using NWheels.UI;
using NWheels.UI.Toolbox;
using NWheels.UI.Uidl;

namespace NWheels.Domains.ECommerce.Tests.App.UI
{
    public class BackOfficeApp : ApplicationBase<BackOfficeApp, Empty.Input, Empty.Data, Empty.State>
    {
        #region Overrides of ApplicationBase<BackOfficeApp,Input,Data,State>

        protected override void DescribePresenter(PresenterBuilder<BackOfficeApp, Empty.Data, Empty.State> presenter)
        {
            DefaultSkin = "Debug";
            SetDefaultInitialScreen(Login);

            presenter.On(Login.LoginForm.UserLoggedIn).Navigate().ToScreen(Portal);
            presenter.On(Portal.CurrentUser.UserLoggedOut).Navigate().ToScreen(Login);
            presenter.On(RequestNotAuthorized).Navigate().ToScreen(Login)
                .Then(b => b.UserAlertFrom<IAlerts>().ShowInline(x => x.LogInToAuthorizeRequestedOperation()));
        }

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------

        public LoginScreen Login { get; set; }
        public BackOfficePortalScreen Portal { get; set; }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        [DefaultCulture("en-US")]
        public interface IAlerts : IUserAlertRepository
        {
            [WarningAlert]
            UidlUserAlert LogInToAuthorizeRequestedOperation();

            [InfoAlert]
            UidlUserAlert FeatureIsNotYetImplemented();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public class LoginScreen : ScreenBase<LoginScreen, Empty.Input, Empty.Data, Empty.State>
        {
            public LoginScreen(string idName, UidlApplication parent)
                : base(idName, parent)
            {
            }

            //-------------------------------------------------------------------------------------------------------------------------------------------------

            protected override void DescribePresenter(PresenterBuilder<LoginScreen, Empty.Data, Empty.State> presenter)
            {
                ContentRoot = LoginForm;
            }

            //-------------------------------------------------------------------------------------------------------------------------------------------------

            public NWheels.Domains.Security.UI.UserLoginForm LoginForm { get; set; }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public class BackOfficePortalScreen : ScreenBase<BackOfficePortalScreen, Empty.Input, Empty.Data, Empty.State>
        {
            public BackOfficePortalScreen(string idName, UidlApplication parent)
                : base(idName, parent)
            {
            }

            //-------------------------------------------------------------------------------------------------------------------------------------------------

            protected override void DescribePresenter(PresenterBuilder<BackOfficePortalScreen, Empty.Data, Empty.State> presenter)
            {
                ContentRoot = Console;
                Console
                    .Dashboard(this.Dashboard)
                    .StatusBarWidgets(CurrentUser)
                    .Navigation(new {
                        #region Navigation

                        Dashboard = Goto(this.Dashboard),
                        Inventory = new {
                            Attributes = Goto(this.Attributes),
                            Products = Goto(this.Products),
                        },
                        Administration = new {
                            UserAccounts = Menu.Action.Goto(this.UserAccounts, this.Console.MainContent),
                        },
                        Logout = Menu.Action.InvokeCommand(CurrentUser.LogOut)

                        #endregion
                    });

                UserAccounts.GridColumns(x => x.LoginName, x => x.FullName, x => x.AssociatedRoles, x => x.LastLoginAtUtc, x => x.IsLockedOut);
            }

            //-------------------------------------------------------------------------------------------------------------------------------------------------

            public BackOfficeApp App { get; set; }
            public ManagementConsole Console { get; set; }
            public CurrentLoggedInUser CurrentUser { get; set; }

            //-------------------------------------------------------------------------------------------------------------------------------------------------
            
            public DashboardScreenPart Dashboard { get; set; }
            public CrudScreenPart<IAttributeEntity> Attributes { get; set; }
            public CrudScreenPart<IProductEntity> Products { get; set; }
            public CrudScreenPart<IUserAccountEntity> UserAccounts { get; set; }

            //-------------------------------------------------------------------------------------------------------------------------------------------------

            private Menu.ItemAction Goto(IScreenPartWithInput<Empty.Input> screenPart, params string[] userRoles)
            {
                return Menu.Action.Goto(screenPart, this.Console.MainContent).UserRoles(userRoles);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public class DashboardScreenPart : ScreenPartBase<DashboardScreenPart, Empty.Input, Empty.Data, Empty.State>
        {
            public DashboardScreenPart(string idName, UidlApplication parent)
                : base(idName, parent)
            {
            }

            //-------------------------------------------------------------------------------------------------------------------------------------------------

            protected override void DescribePresenter(PresenterBuilder<DashboardScreenPart, Empty.Data, Empty.State> presenter)
            {
            }
        }
    }
}
