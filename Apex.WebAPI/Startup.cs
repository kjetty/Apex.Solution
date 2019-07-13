using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Apex.WebAPI.Startup))]

namespace Apex.WebAPI
{
	public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
