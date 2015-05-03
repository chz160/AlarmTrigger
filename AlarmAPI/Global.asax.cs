using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AlarmAPI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static AlarmTwitterStreamClass alarm;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            alarm = new AlarmTwitterStreamClass();

            Task.Factory.StartNew(() => alarm.Start());
        }

        
    }
}
