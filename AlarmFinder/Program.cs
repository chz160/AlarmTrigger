using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AlarmFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            const string subnet = "10.1.8.";
            const int port = 8000;
            const int lower = 1;
            const int upper = 254;
            var threadList = new List<Task>();
            Console.WriteLine("Searching...");

            var cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            for (var i = lower; i <= upper; i++)
            {
                var lastSection = i;
                threadList.Add(Task.Factory.StartNew(() =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    using (var client = new CustomWebClient())
                    {
                        client.Timeout = 500;
                        try
                        {
                            var ip = string.Format("{0}{1}", subnet, lastSection);
                            var url = string.Format("http://{0}:{1}/status", ip, port);
                            Console.WriteLine("Checking ip address {0}", ip);
                            var data = client.DownloadString(new Uri(url));
                            if (!string.IsNullOrEmpty(data))
                            {
                                var status = JsonConvert.DeserializeObject<Status>(data);
                                if (status != null && status.Message == "Alarm Is Up")
                                {
                                    Console.WriteLine("Alarm found at {0}", ip);
                                    cancellationTokenSource.Cancel();
                                }
                            }
                        }
                        catch (WebException)
                        { }
                    }
                }, cancellationToken));
            }
            try
            {
                Task.WaitAll(threadList.ToArray());
            }
            catch (AggregateException)
            {}

            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }
    }
}
