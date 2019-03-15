using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LS_Shop.Startup))]
namespace LS_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
