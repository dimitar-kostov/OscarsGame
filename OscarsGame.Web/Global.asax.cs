using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using Microsoft.Practices.Unity;

namespace OscarsGame
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            Application.Lock();
            try
            {
                var myContainer = Application["EntLibContainer"] as IUnityContainer;
                if (myContainer == null)
                {
                    myContainer = new UnityContainer();
                    // myContainer.AddExtension(new EnterpriseLibraryCoreExtension());

                    WebContainerManager webDataContainerManager = new WebContainerManager();
                    webDataContainerManager.RegisterTypes(myContainer);

                    // Add your own custom registrations and mappings here as required
                    Application["EntLibContainer"] = myContainer;
                }
            }
            finally
            {
                Application.UnLock();
            }

            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );

        }


    }
}