using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlarmAPI.Startup))]
namespace AlarmAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
