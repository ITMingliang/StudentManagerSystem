using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentManager_MVC5.Startup))]
namespace StudentManager_MVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
