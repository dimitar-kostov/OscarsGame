using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using OscarsGame.Business.Interfaces;
using OscarsGame.Web.Identity;
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

            if (!Page.IsPostBack)
            {
                LoginView1.DataBind();
            }

            SetControlsVisibility();
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

        private void SetControlsVisibility()
        {
            bool isProxiadClient = IdentityHelper.IsProxiadClient();

            PrivacyPolicyLink.Visible = !isProxiadClient;
            TermsOfServiceLink.Visible = !isProxiadClient;
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(
                CookieAuthenticationDefaults.AuthenticationType,
                DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);
        }

        protected string GetLoginLabel()
        {
            return IdentityHelper.IsProxiadClient() ? "Log in with Office 365" : "Log in";
        }

        protected string GetLoginUrl()
        {
            return $"~/Account/Login?ReturnUrl={ new Uri(Request.Url.PathAndQuery, UriKind.Relative) }";
        }

        protected string GetUserDisplayName()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var currentUser = manager.FindById(Context.User.Identity.GetUserId().ToGuid());
            return currentUser.DisplayName;

            //return Context.User.Identity.GetOpenIdName();
        }

        protected string GetManageUrl()
        {
            return IdentityHelper.IsProxiadClient() ? "~/Account/ManageOpenId" : "~/Account/Manage";
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