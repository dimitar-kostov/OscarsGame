using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OpenIdConnect;
using OscarsGame.Web.Identity;
using Owin;
using System;
using System.Configuration;

namespace OscarsGame
{
    public partial class Startup
    {

        private string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private string RedirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        private string GoogleClientId = ConfigurationManager.AppSettings["Google:ClientId"];
        private string GoogleClientSecret = ConfigurationManager.AppSettings["Google:ClientSecret"];

        private string FacebookAppId = ConfigurationManager.AppSettings["Facebook:AppId"];
        private string FacebookAppSecret = ConfigurationManager.AppSettings["Facebook:AppSecret"];

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301883
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(IdentityConfig.CreateUnitOfWork);
            app.CreatePerOwinContext<ApplicationUserManager>(IdentityConfig.CreateApplicationUserManager);
            app.CreatePerOwinContext<ApplicationSignInManager>(IdentityConfig.CreateApplicationSignInManager);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, IdentityUser, Guid>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie),
                        getUserIdCallback: (claimsIdentity) => claimsIdentity.GetUserId().ToGuid())
                }
            });

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            //lockoutapp.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            if (FacebookAppId != null && FacebookAppSecret != null)
            {
                app.UseFacebookAuthentication(
                   appId: FacebookAppId,
                   appSecret: FacebookAppSecret
                );
            }

            if (GoogleClientId != null && GoogleClientSecret != null)
            {
                app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
                {
                    ClientId = GoogleClientId,
                    ClientSecret = GoogleClientSecret
                });
            }

            if (IdentityHelper.IsProxiadClient())
            {
                string authority = aadInstance + tenantId;
                app.UseOpenIdConnectAuthentication(
                    new OpenIdConnectAuthenticationOptions
                    {
                        ClientId = clientId,
                        Authority = authority,
                        PostLogoutRedirectUri = postLogoutRedirectUri,
                        RedirectUri = RedirectUri,
                        Scope = "./Default",

                        Notifications = new OpenIdConnectAuthenticationNotifications()
                        {
                            AuthenticationFailed = (context) =>
                            {
                                return System.Threading.Tasks.Task.FromResult(0);
                            }
                        }

                    }
                );
            }

            // This makes any middleware defined above this line run before the Authorization rule is applied in web.config
            app.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}
