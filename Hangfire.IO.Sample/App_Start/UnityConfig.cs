using Hangfire.Sample.Repository.EF;
using Hangfire.Sample.Repository.Identity;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using System;
using System.Web;

namespace Hangfire.IO.Sample
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            var log = LogManager.GetLogger("Application");
            container.RegisterType<ILog>().RegisterInstance(log);

            container.RegisterType<ApplicationDbContext>(new PerThreadLifetimeManager());

            SetupIdentityServices(container);

            


        }
        private static void SetupIdentityServices(IUnityContainer container)
        {
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<ApplicationRoleManager>();
            container.RegisterType<IUserStore<ApplicationUser, Guid>, AppUserStore>(new InjectionConstructor(typeof(ApplicationDbContext)));
            container.RegisterType<IRoleStore<AppRole, Guid>, AppRoleStore>(new InjectionConstructor(typeof(ApplicationDbContext)));
            container.RegisterType<IAuthenticationManager>(new PerResolveLifetimeManager(), new InjectionFactory(con => HttpContext.Current.GetOwinContext().Authentication));
        }
    }
}
