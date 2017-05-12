using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MomoSecretSociety.Startup))]
namespace MomoSecretSociety
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
