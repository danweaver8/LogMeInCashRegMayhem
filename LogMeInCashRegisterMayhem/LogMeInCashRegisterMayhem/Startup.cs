using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogMeInCashRegisterMayhem.Startup))]
namespace LogMeInCashRegisterMayhem
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
