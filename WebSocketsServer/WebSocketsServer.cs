using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace waltonstine.websockets
{
    public class WebSocketsService
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start WebSockets Service");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
