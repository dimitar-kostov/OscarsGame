using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using OscarsGame.Business.Interfaces;
using OscarsGame.Extensions;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OscarsGame
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        private readonly IGamePropertyService GamePropertyService;
        private readonly ICategoryService CategoryService;

        public SiteMaster(
            IGamePropertyService gamePropertyService,
            ICategoryService categoryService)
        {
            GamePropertyService = gamePropertyService;
            CategoryService = categoryService;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Admin.Visible = HttpContext.Current.User.IsInRole("admin");

            if (GamePropertyService.IsGameStopped())
            {
                lblRemaining.Text = string.Empty;
            }
            else
            {
                lblRemaining.Text = GetRemainingTimeLabel();
            }

            stopGameLabel.Text = ShowGameStatus();
        }

        private string GetRemainingTimeLabel()
        {
            DateTime endDate = GamePropertyService.GetGameStopDate();
            TimeSpan tsRemainingTime = endDate - DateTime.Now;

            return string.Format("Remaining time for voting: {0} {1} {2}",
                tsRemainingTime.Days == 1 ? "1 Day" : tsRemainingTime.Days + " Days",
                tsRemainingTime.Hours == 1 ? "1 Hour" : tsRemainingTime.Hours + " Hours",
                tsRemainingTime.Minutes == 1 ? "1 Minute" : tsRemainingTime.Minutes + " Minutes");
        }

        public string ShowGameStatus()
        {
            if (GamePropertyService.IsGameStopped())
            {
                return "The Game is stopped!";
            }
            else if (GamePropertyService.IsGameNotStartedYet())
            {
                return "The Game is not started yet";
            }
            else
            {
                return "The Game is running!";
            }
        }


        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
        }

        protected string GetOpenIdUserName()
        {
            return Context.User.Identity.GetOpenIdName();
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = CategoryService;
        }

        public string GetCategoryUrl(int categoryId)
        {
            return String.Format("~/CommonPages/ShowCategory?ID={0}", categoryId);
        }
    }

}