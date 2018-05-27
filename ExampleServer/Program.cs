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

            //Start listening for requests
            server.Run();
        }
    }
}
