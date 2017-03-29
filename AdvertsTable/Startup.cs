using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdvertsTable.Startup))]
namespace AdvertsTable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
