using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NCDNewMIS.Startup))]
namespace NCDNewMIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
