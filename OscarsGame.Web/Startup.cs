using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OscarsGame.Startup))]
namespace OscarsGame
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
