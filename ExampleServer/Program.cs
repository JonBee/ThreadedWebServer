using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JonBee.ThreadedWebServer;

namespace ExampleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadedWebServer server = new ThreadedWebServer(new List<string>() { "http://localhost:4224/" });
            server.RegisterDefaultHandler(new FileHandler());
            server.Run();
        }
    }

    public class FileHandler : RequestHandler
    {
        public override WebServerResponse HandleRequest(HttpListenerRequest request)
        {
            WebServerResponse response = new WebServerResponse("<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset = UTF-8\"></head><body><p>Hi there</p></body></html>");
            Console.WriteLine(request.Url.LocalPath);
            return response;
        }
    }
}
