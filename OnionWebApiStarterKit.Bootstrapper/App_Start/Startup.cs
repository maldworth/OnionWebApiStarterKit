using Owin;

namespace OnionWebApiStarterKit.Bootstrapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();
            //log4net.GlobalContext.Properties["TrackingId"] = "My Tracking Id zz1";
            //ConfigureAuth(app);
        }
    }
}
