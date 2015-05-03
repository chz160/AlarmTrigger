using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Tweetinvi;
using Newtonsoft.Json;
namespace AlarmAPI
{
    [HubName("twitterUpdateHub")]
    public class TwitterUpdateHub : Hub
    {
        public static void SendNewTweet(Tweetinvi.Core.Interfaces.ITweet tweet)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TwitterUpdateHub>();
            var domainTweet = new
            {
               tweet.Text,
               tweet.Source,
               tweet.Creator.Name
            };
            hubContext.Clients.All.sendNewTweet(domainTweet);
        }
    }
}