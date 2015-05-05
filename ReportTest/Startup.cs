using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReportTest.Startup))]
namespace ReportTest
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
