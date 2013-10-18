using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CellarBotHome.Startup))]
namespace CellarBotHome
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
