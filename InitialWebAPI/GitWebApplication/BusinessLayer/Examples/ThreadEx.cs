using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Examples
{
    class ThreadEx
    {

        private static void DownloadASynchronously_parallel()
        {
            string[] urls =
            {
                "http://www.pluralsight-training.net/microsoft",
                "http://www.microsoft.com/en/us/default.aspx",
                "http://twitter.com/odetocode"
            };

            Parallel.ForEach(urls, url => 
            {
                var client = new WebClient();
                var html = client.DownloadString(url.ToString());
                Console.WriteLine("Download {0} chars from {1} on thread {2}",
                    html.Length, url, Thread.CurrentThread.ManagedThreadId);
            });

        }

        private static void DownloadASynchronously_async()
        {
            string[] urls =
            {
                "http://www.pluralsight-training.net/microsoft",
                "http://www.microsoft.com/en/us/default.aspx",
                "http://twitter.com/odetocode"
            };

            foreach (var url in urls)
            {
                Download_async(url);
            }
        }

        private static void Download_async(string url)
        {
            var client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new Uri(url), url);
        }

        private static void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var html = e.Result;
            var url = e.UserState as string;

            Console.WriteLine("Download {0} chars from {1} on thread {2}",
                html.Length, url, Thread.CurrentThread.ManagedThreadId);
        }

        private static void DownloadASynchronously_threads()
        {
            string[] urls =
            {
                "http://www.pluralsight-training.net/microsoft",
                "http://www.microsoft.com/en/us/default.aspx",
                "http://twitter.com/odetocode"
            };

            foreach (var url in urls)
            {
                var thread = new Thread(Download_thread);
                thread.Start(url);
            }
        }

        private static void Download_thread(object url)
        {
            var client = new WebClient();
            var html = client.DownloadString(url.ToString());
            Console.WriteLine("Download {0} chars from {1} on thread {2}",
                html.Length, url, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
