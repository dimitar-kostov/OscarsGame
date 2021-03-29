using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using OscarsGame.Data;
using OscarsGame.Domain;
using OscarsGame.Web.Identity;

namespace OscarsGame
{
    public static class IdentityConfig
    {
        public static IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork("DefaultConnection");
        }

        public static ApplicationUserManager CreateApplicationUserManager(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            return new ApplicationUserManager(
                new UserStore(context.Get<IUnitOfWork>()),
                options.DataProtectionProvider);
        }

        public static ApplicationSignInManager CreateApplicationSignInManager(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(
                context.Get<ApplicationUserManager>(),
                context.Authentication);
        }
    }
}
