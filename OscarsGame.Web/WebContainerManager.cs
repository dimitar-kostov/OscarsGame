using OscarsGame.Business;
using OscarsGame.Data;
using Unity;

namespace OscarsGame
{
    public static class WebContainerManager
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            BusinessContainerManager.RegisterTypes(container);

            // Register data types
            DataContainerManager.RegisterTypes(container);
        }
    }
}