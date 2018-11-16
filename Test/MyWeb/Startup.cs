using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebJobPortal.Startup))]
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
