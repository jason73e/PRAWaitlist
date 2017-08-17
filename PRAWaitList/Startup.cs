using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PRAWaitList.Startup))]
namespace PRAWaitList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
