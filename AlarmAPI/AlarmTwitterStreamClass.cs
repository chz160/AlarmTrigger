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
        private const string ConsumerKey = "bNvDEkGGmyXlnHhi5b6utXfVF";
        private const string ConsumerSecret = "kqaBC51XxmeXT5lekUlXSKCqDRxc23cLcFStsvgNTYdlQ4EkCt";
        private const string UserToken = "16899512-aakZosldVAApq75zwipk5hQ2bdgxbJAAGdHviIV75";
        private const string UserSecret = "w00NwK7WbNOZlIwrvCgiVzt4QGVY6TCDPolLQKIQZtP6L";

        private readonly Tweetinvi.Core.Interfaces.Streaminvi.IFilteredStream _filteredStream;

        public AlarmTwitterStreamClass()
        {
            _filteredStream = Stream.CreateFilteredStream();
            TwitterCredentials.SetCredentials(UserToken, UserSecret, ConsumerKey, ConsumerSecret);
        }

        public void Start()
        {
            StartTwitterStream("#HT7");
        }

        private void StartTwitterStream(String hashtag)
        {
            _filteredStream.AddTrack(hashtag);
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
    }
}