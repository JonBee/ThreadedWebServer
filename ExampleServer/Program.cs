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
            //Initialize a new ThreadedWebServer instance and configure it to listen for all requests at http://localhost:4224/
            ThreadedWebServer server = new ThreadedWebServer(new List<string>() { "http://localhost:4224/" });

            //Tell the server to use the built-in File Handler by default if no other handler can be found.
            //The FileHandler automatically presents files if they are found, and lists directory contents if told to do so.
            server.RegisterDefaultHandler(new FileHandler(true));

            //Register our custom handler so that it responds to requests for root or "index.html"
            server.RegisterHandler("", new CustomRequestHandler());
            server.RegisterHandler("index.html", new CustomRequestHandler());

            //Start listening for requests
            Console.WriteLine("Webserver running...");
            server.Run();
        }
    }

    /// <summary>
    /// This is our custom RequestHandler, it supplies a basic html page when it receives a request
    /// </summary>
    class CustomRequestHandler : RequestHandler
    {
        //Override this method to enable your custom logic to respond to the request
        public override WebServerResponse HandleRequest(HttpListenerRequest request)
        {
            string html = @"
                <html>
                    <head>
                        <meta http-equiv=""content - type"" content=""text/html; charset = UTF-8\"">
                        <title>Custom request handler</title>
                      </head>
                    <body>
                        <p>Hello, World!</p>
                    </body>
                </html>
            ";
            return new WebServerResponse(html);
        }
    }
}
