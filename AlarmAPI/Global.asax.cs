using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tweetinvi;

namespace AlarmAPI
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Task.Factory.StartNew(() => StartTwitterStream());

        }

        private void StartTwitterStream()
        {
            string consumerKey = "bNvDEkGGmyXlnHhi5b6utXfVF";
            string consumerSecret = "kqaBC51XxmeXT5lekUlXSKCqDRxc23cLcFStsvgNTYdlQ4EkCt";

            TwitterCredentials.SetCredentials("16899512-aakZosldVAApq75zwipk5hQ2bdgxbJAAGdHviIV75", "w00NwK7WbNOZlIwrvCgiVzt4QGVY6TCDPolLQKIQZtP6L", consumerKey, consumerSecret);

            var filteredStream = Tweetinvi.Stream.CreateFilteredStream();
            filteredStream.AddTrack("#PacquiaoMayweather");
            filteredStream.MatchingTweetReceived += (sender, args) =>
            {
                AlarmAPI.TwitterUpdateHub.SendNewTweet(args.Tweet);
            };
            filteredStream.StartStreamMatchingAllConditions();
        }

        private void ToggleLight()
        {
            Task.Factory.StartNew(() =>
            {
                using (var client = new WebClient())
                {
                    client.DownloadString("http://10.10.1.184:8000/on");
                    Thread.Sleep(5000);
                    client.DownloadString("http://10.10.1.184:8000/off");
                }
            });
        }
    }
}
