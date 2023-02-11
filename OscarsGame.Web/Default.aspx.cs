using OscarsGame.Web.Identity;
using System;
using System.Web.UI;

namespace OscarsGame
{
    public partial class Default : Page
    {
        private readonly string[] Posters = 
        {
            "images/Posters2021/Oscars2021_BlueWallpaper.webp",
            "images/Posters2021/Oscars2021_GreenWallpaper.webp",
            "images/Posters2021/Oscars2021_OrangeWallpaper.webp",
            "images/Posters2021/Oscars2021_PinkWallpaper.webp",
            "images/Posters2021/Oscars2021_PurpleWallpaper.webp",
            "images/Posters2021/Oscars2021_RainbowWallpaper.webp",
            "images/Posters2021/Oscars2021_YellowWallpaper.webp",
        };

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

        public string GetPosterUrl()
        {
            var rnd = new Random();
            return Posters[rnd.Next(Posters.Length)];
        }
    }
}