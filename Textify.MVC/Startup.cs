using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Textify.MVC.Startup))]
namespace Textify.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
