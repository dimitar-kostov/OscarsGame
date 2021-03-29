using OscarsGame.Business;
using OscarsGame.Data;
using OscarsGame.Domain;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace OscarsGame
{
    public static class WebContainerManager
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            // Register data types

            container.RegisterType<IUnitOfWork, UnitOfWork>(
                new PerThreadLifetimeManager(),
                new InjectionConstructor("DefaultConnection"));


            DataContainerManager.RegisterTypes(container);

            // Register service types
            BusinessContainerManager.RegisterTypes(container);
        }
    }
}