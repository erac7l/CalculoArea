using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalculoArea.Startup))]
namespace CalculoArea
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
