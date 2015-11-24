using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartSpaceWeb.Startup))]
namespace SmartSpaceWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
