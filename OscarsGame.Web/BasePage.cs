namespace OscarsGame
{
    public class BasePage : System.Web.UI.Page
    {
        protected bool CheckIfTheUserIsLogged()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
