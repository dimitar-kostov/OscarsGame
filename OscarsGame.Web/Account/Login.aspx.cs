using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using OscarsGame.Web.Identity;
using System;
using System.Globalization;
using System.Web;
using System.Web.UI;

namespace OscarsGame.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var returnUrl = Request.QueryString["ReturnUrl"];

            if (IdentityHelper.IsProxiadClient())
            {
                string provider = OpenIdConnectAuthenticationDefaults.AuthenticationType;

                string redirectUrl = ResolveUrl(
                    String.Format(CultureInfo.InvariantCulture, "~/Account/RegisterExternalLogin?{0}={1}&returnUrl={2}",
                        IdentityHelper.ProviderNameKey, provider, returnUrl));

                Context.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = redirectUrl },
                    provider);

                Response.StatusCode = 401;
                Response.End();
            }
            else
            {
                RegisterHyperLink.NavigateUrl = "Register";
                // Enable this once you have account confirmation enabled for password reset functionality
                //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
                OpenAuthLogin.ReturnUrl = returnUrl;
                var encodedReturnUrl = HttpUtility.UrlEncode(returnUrl);

                if (!String.IsNullOrEmpty(encodedReturnUrl))
                {
                    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + encodedReturnUrl;
                }
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                        Request.QueryString["ReturnUrl"],
                                                        RememberMe.Checked),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }
    }
}