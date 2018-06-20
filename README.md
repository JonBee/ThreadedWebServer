# ThreadedWebServer
A library that exposes a small, easy-to-use, and performant webserver for use in .NET applications

## Usage
Using the library is extremely easy and can be done with only three lines of code (if you don't want it to do much)

Include the library:
```c#
using JonBee.ThreadedWebServer;
```
Create a new instance of the server and run it:
```c#
ThreadedWebServer server = new ThreadedWebServer(new List<string>() { "http://localhost:4224/" });
server.Run();
```
This creates a server that listens for all requests to http://localhost:4224 and then uses a built-in RequestHandler to serve up files.
It will attempt to serve files relative to the executable directory.