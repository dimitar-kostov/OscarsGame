using OscarsGame.Web.Identity;
using System;
using System.Web.UI;

namespace OscarsGame
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IdentityHelper.IsProxiadClient())
            {
                LiteralProxiad.Visible = true;
                LiteralDefault.Visible = false;
            }
            else
            {
                LiteralProxiad.Visible = false;
                LiteralDefault.Visible = true;

            }
        }
    }
}