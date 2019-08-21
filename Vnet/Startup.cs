using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vnet.Startup))]
namespace Vnet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
