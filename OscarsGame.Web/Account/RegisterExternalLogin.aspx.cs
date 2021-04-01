using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OscarsGame.Web.Identity;
using System;
using System.Security.Claims;
using System.Web;

namespace OscarsGame.Account
{
    public partial class RegisterExternalLogin : System.Web.UI.Page
    {
        protected string ProviderName
        {
            get { return (string)ViewState["ProviderName"] ?? String.Empty; }
            private set { ViewState["ProviderName"] = value; }
        }

        protected string ProviderAccountKey
        {
            get { return (string)ViewState["ProviderAccountKey"] ?? String.Empty; }
            private set { ViewState["ProviderAccountKey"] = value; }
        }

        private void RedirectOnFail()
        {
            Response.Redirect((User.Identity.IsAuthenticated) ? "~/Account/Manage" : "~/Account/Login");
        }

        protected void Page_Load()
        {
            // Process the result from an auth provider in the request
            ProviderName = IdentityHelper.GetProviderNameFromRequest(Request);
            if (String.IsNullOrEmpty(ProviderName))
            {
                RedirectOnFail();
                return;
            }
            if (!IsPostBack)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                var loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo();
                if (loginInfo == null)
                {
                    RedirectOnFail();
                    return;
                }
                var user = manager.Find(loginInfo.Login);
                if (user != null)
                {
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else if (User.Identity.IsAuthenticated)
                {
                    // Apply Xsrf check when linking
                    var verifiedloginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo(IdentityHelper.XsrfKey, User.Identity.GetUserId());
                    if (verifiedloginInfo == null)
                    {
                        RedirectOnFail();
                        return;
                    }

                    var result = manager.AddLogin(User.Identity.GetUserId().ToGuid(), verifiedloginInfo.Login);
                    if (result.Succeeded)
                    {
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    }
                    else
                    {
                        AddErrors(result);
                        return;
                    }
                }
                else if (IdentityHelper.IsProxiadClient())
                {
                    string proxiadEmailDomain = "@proxiad.com";
                    string identityUserName = null;

                    if (loginInfo.Email != null
                        && loginInfo.Email.EndsWith(proxiadEmailDomain))
                    {
                        identityUserName = loginInfo.Email;
                    }
                    else if (loginInfo.DefaultUserName != null
                        && loginInfo.DefaultUserName.EndsWith(proxiadEmailDomain))
                    {
                        identityUserName = loginInfo.DefaultUserName;
                    }

                    if (identityUserName != null)
                    {
                        CreateAndLoginUser(identityUserName, identityUserName);
                    }

                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    email.Text = loginInfo.Email;
                }
            }
        }

        protected void LogIn_Click(object sender, EventArgs e)
        {
            CreateAndLoginUser();
        }

        private void CreateAndLoginUser()
        {
            if (!IsValid)
            {
                return;
            }

            CreateAndLoginUser(email.Text, email.Text);
        }

        private void CreateAndLoginUser(string identityUserName, string identityEmail)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            var user = new IdentityUser()
            {
                UserName = identityUserName,
                Email = identityEmail,
                DisplayName = identityUserName
            };

            IdentityResult result = manager.Create(user);
            if (result.Succeeded)
            {
                var loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo();
                if (loginInfo == null)
                {
                    RedirectOnFail();
                    return;
                }
                result = manager.AddLogin(user.Id, loginInfo.Login);
                if (result.Succeeded)
                {
                    string displayName = null;

                    if (loginInfo.ExternalIdentity.HasClaim(c => c.Type == "name"))
                    {
                        var claim = loginInfo.ExternalIdentity.FindFirst("name");

                        manager.AddClaim(user.Id, claim);
                        displayName = claim.Value;
                    }

                    if (loginInfo.ExternalIdentity.HasClaim(c => c.Type == ClaimTypes.Name))
                    {
                        var claim = loginInfo.ExternalIdentity.FindFirst(ClaimTypes.Name);

                        manager.AddClaim(user.Id, claim);
                        displayName = claim.Value;
                    }

                    if (loginInfo.ExternalIdentity.HasClaim(c => c.Type == ClaimTypes.GivenName))
                    {
                        manager.AddClaim(user.Id,
                            loginInfo.ExternalIdentity.FindFirst(ClaimTypes.GivenName));
                    }

                    if (loginInfo.ExternalIdentity.HasClaim(c => c.Type == ClaimTypes.Surname))
                    {
                        manager.AddClaim(user.Id,
                            loginInfo.ExternalIdentity.FindFirst(ClaimTypes.Surname));
                    }

                    if (!string.IsNullOrEmpty(displayName))
                    {
                        user.DisplayName = displayName;
                        manager.Update(user);
                    }

                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // var code = manager.GenerateEmailConfirmationToken(user.Id);
                    // Send this link via email: IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id)

                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    return;
                }
            }
            AddErrors(result);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}