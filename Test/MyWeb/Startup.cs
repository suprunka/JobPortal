using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebJobPortal.Startup))]
namespace WebJobPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
