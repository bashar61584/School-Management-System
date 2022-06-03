using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolMS.Startup))]
namespace SchoolMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
