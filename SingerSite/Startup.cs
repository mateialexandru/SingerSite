using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SingerSite.Startup))]
namespace SingerSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
