using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleCapaApp.Startup))]
namespace SimpleCapaApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
