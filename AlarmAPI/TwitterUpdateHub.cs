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

            if (tweet != null)
            {
                var html = "";
                var embedObject = tweet.GenerateOEmbedTweet();
                if (embedObject != null)
                {
                    html = embedObject.HTML;
                    var domainTweet = new
                    {
                        tweet.Text,
                        tweet.Source,
                        tweet.Creator.Name,
                        HTML = html
                    };
                    hubContext.Clients.All.sendNewTweet(domainTweet);
                }
                    
            }
        }
    }
}