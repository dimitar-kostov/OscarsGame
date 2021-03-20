using Microsoft.Practices.Unity;
using OscarsGame.Business.Interfaces;

namespace OscarsGame
{
    public class BasePage : System.Web.UI.Page
    {
        protected T GetBuisnessService<T>()
        {
            IUnityContainer container = (IUnityContainer)Application["EntLibContainer"];
            return container.Resolve<T>();
        }

        protected bool IsGameNotStartedYet()
        {
            return GetBuisnessService<IGamePropertyService>().IsGameNotStartedYet();
        }

        protected bool IsGameRunning()
        {
            return !GetBuisnessService<IGamePropertyService>().IsGameStopped();
        }

        protected bool CheckIfTheUserIsLogged()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
