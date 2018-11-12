using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(MyTurn.Web.Startup))]
namespace MyTurn.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var appSettings = ConfigurationManager.AppSettings;
        }
    }
}
