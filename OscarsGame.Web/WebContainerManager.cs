using OscarsGame.Business;
using Unity;

namespace OscarsGame
{
    public class WebContainerManager
    {
        public void RegisterTypes(IUnityContainer container)
        {
            BusinessContainerManager containerManager = new BusinessContainerManager();
            containerManager.RegisterTypes(container);
        }
    }
}