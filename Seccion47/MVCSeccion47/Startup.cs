using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCSeccion47.Startup))]
namespace MVCSeccion47
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
