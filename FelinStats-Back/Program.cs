using Nancy.Hosting.Self;
using System;
using System.Threading;

namespace FelinStats_Back
{
    class Program
    {
        private static AutoResetEvent autoEvent = new AutoResetEvent(false);

        public static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(LaunchServer), autoEvent);
            autoEvent.WaitOne();
        }

        public static void LaunchServer(object stateInfo)
        {
            HostConfiguration config = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
            NancyHost host = new NancyHost(config, new Uri("http://localhost:8082/"));
            host.Start();
            Console.WriteLine("Host Started... Do ^C to exit.");
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("^C received, exitting...");
                host.Dispose();
                ((AutoResetEvent)stateInfo).Set();
            };
        }
    }
}
