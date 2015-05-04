using System;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using System.Net;
using Tweetinvi.Core.Interfaces.Streaminvi;

namespace AlarmAPI
{
    public class AlarmTwitterStreamClass
    {
        private const string ConsumerKey = "bNvDEkGGmyXlnHhi5b6utXfVF";
        private const string ConsumerSecret = "kqaBC51XxmeXT5lekUlXSKCqDRxc23cLcFStsvgNTYdlQ4EkCt";
        private const string UserToken = "16899512-aakZosldVAApq75zwipk5hQ2bdgxbJAAGdHviIV75";
        private const string UserSecret = "w00NwK7WbNOZlIwrvCgiVzt4QGVY6TCDPolLQKIQZtP6L";
        private readonly string _url = "";

        private readonly IFilteredStream _filteredStream;

        public AlarmTwitterStreamClass()
        {
            var ipAddress = System.Configuration.ConfigurationManager.AppSettings["IpAddress"];
            _url = string.Format("http://{0}:8000", ipAddress);
            _filteredStream = Stream.CreateFilteredStream();
            TwitterCredentials.SetCredentials(UserToken, UserSecret, ConsumerKey, ConsumerSecret);
        }

        public void Start()
        {
            StartTwitterStream("#MayThe4thBeWithYou");
        }

        private void StartTwitterStream(String hashtag)
        {
            _filteredStream.AddTrack(hashtag);
            _filteredStream.AddCustomQueryParameter("include_rts", "true");
            _filteredStream.MatchingTweetAndLocationReceived -= _filteredStream_MatchingTweetAndLocationReceived;
            _filteredStream.MatchingTweetAndLocationReceived += _filteredStream_MatchingTweetAndLocationReceived;
            _filteredStream.StartStreamMatchingAllConditions();
        }

        private void _filteredStream_MatchingTweetAndLocationReceived(object sender, Tweetinvi.Core.Events.EventArguments.MatchedTweetAndLocationReceivedEventArgs e)
        {
            TwitterUpdateHub.SendNewTweet(e.Tweet);
            ToggleLight();
        }

        private void ToggleLight()
        {
            Task.Factory.StartNew(() =>
            {
                using (var client = new WebClient())
                {
                    client.DownloadString(string.Format("{0}/on", _url));
                    Thread.Sleep(5000);
                    client.DownloadString(string.Format("{0}/off", _url));
                }
            });
        }

        public void GlobalUpdateHashTag(String hashtag)
        {
            _filteredStream.StopStream();
            _filteredStream.ClearTracks();
            StartTwitterStream(hashtag);
        }
    }
}