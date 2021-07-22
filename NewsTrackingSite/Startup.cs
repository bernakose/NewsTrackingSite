using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsTrackingSite.Startup))]
namespace NewsTrackingSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
