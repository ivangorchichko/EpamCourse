using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Task5.WEB.Startup))]
namespace Task5.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
