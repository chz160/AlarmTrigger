using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using System.Net;

namespace AlarmAPI
{
    public class AlarmTwitterStreamClass
    {
        private Tweetinvi.Core.Interfaces.Streaminvi.IFilteredStream _filteredStream;

        CancellationTokenSource _cts = new CancellationTokenSource();

        public AlarmTwitterStreamClass()
        {
            _filteredStream = Tweetinvi.Stream.CreateFilteredStream();
            string consumerKey = "bNvDEkGGmyXlnHhi5b6utXfVF";
            string consumerSecret = "kqaBC51XxmeXT5lekUlXSKCqDRxc23cLcFStsvgNTYdlQ4EkCt";

            TwitterCredentials.SetCredentials("16899512-aakZosldVAApq75zwipk5hQ2bdgxbJAAGdHviIV75", "w00NwK7WbNOZlIwrvCgiVzt4QGVY6TCDPolLQKIQZtP6L", consumerKey, consumerSecret);

        }

        public void StartTwitterStream(String hashtag)
        {
            _filteredStream.AddTrack(hashtag);


            _filteredStream.MatchingTweetAndLocationReceived -= _filteredStream_MatchingTweetAndLocationReceived;


            _filteredStream.MatchingTweetAndLocationReceived += _filteredStream_MatchingTweetAndLocationReceived;

            _filteredStream.StartStreamMatchingAllConditions();
        }

        void _filteredStream_MatchingTweetAndLocationReceived(object sender, Tweetinvi.Core.Events.EventArguments.MatchedTweetAndLocationReceivedEventArgs e)
        {
            AlarmAPI.TwitterUpdateHub.SendNewTweet(e.Tweet);
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

        public void GlobalUpdateHashTag(String hashtag)
        {
            _filteredStream.StopStream();
            _filteredStream.ClearTracks();
            StartTwitterStream(hashtag);
        }

        public void Start()
        {
            StartTwitterStream("#PacquiaoMayweather");
        }
    }
}