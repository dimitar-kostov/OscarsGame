using Microsoft.AspNet.Identity.Owin;
using OscarsGame.Business;
using OscarsGame.Data;
using OscarsGame.Domain;
using OscarsGame.Web.Identity;
using System.Web;
using Unity;
using Unity.Injection;

namespace OscarsGame
{
    public static class WebContainerManager
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork>(
                new InjectionFactory(x =>
                    HttpContext.Current
                               .GetOwinContext()
                               .Get<IUnitOfWork>()));

            container.RegisterType<ApplicationUserManager>(
                new InjectionFactory(x =>
                    HttpContext.Current
                               .GetOwinContext()
                               .Get<ApplicationUserManager>()));

            container.RegisterType<ApplicationSignInManager>(
                new InjectionFactory(x =>
                    HttpContext.Current
                               .GetOwinContext()
                               .Get<ApplicationSignInManager>()));

            DataContainerManager.RegisterTypes(container);
            BusinessContainerManager.RegisterTypes(container);
        }
    }
}