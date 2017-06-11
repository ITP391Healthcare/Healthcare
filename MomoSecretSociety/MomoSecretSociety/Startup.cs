using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Web;

[assembly: OwinStartupAttribute(typeof(MomoSecretSociety.Startup))]
namespace MomoSecretSociety
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);


        }
    }
}
