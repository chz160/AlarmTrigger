using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlarmFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var lower = 1;
            var upper = 254;
            var threadList = new List<Task>();
            for (int i = 1; i <= upper; i++)
            {
                threadList.Add(
                    Task.Factory.StartNew(() =>
                    {
                        using (var client = new WebClient())
                        {
                            var url = string.Format("http://10.10.1.{0}", i);
                            var data = client.DownloadString(new Uri(url));
                            
                        }
                    })
                );
            }
            
        }
    }
}
