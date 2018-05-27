# ThreadedWebServer
A library that exposes a small, easy-to-use, and performant webserver for use in .NET applications

## Usage
Using the library is extremely easy and can be done with only three lines of code (if you don't want it to do much)

Include the library:
```csharp
using JonBee.ThreadedWebServer;
```
Create a new instance of the server and run it:
```csharp
ThreadedWebServer server = new ThreadedWebServer(new List<string>() { "http://localhost:4224/" });
server.Run();
```
This creates a server that listens for all requests to http://localhost:4224 and then uses a built-in RequestHandler to serve up files.
It will attempt to serve files relative to the executable directory.

## Custom Requests
To handle requests programmatically as opposed to letting the server look for and serve files, you have to define a class that inherits from RequestHandler and then override it's HandleRequest method:
```csharp
class MyCustomRequestHandler : RequestHandler
{
  public override WebServerResponse HandleRequest(HttpListenerRequest request)
  {
    return new WebServerResponse("<html>...</html>");
  }
}
```
Once you've created a custom HandleRequest, you need to register it with your server as documented in the section below.

## Registering Handlers
To enable your custom handlers to respond to requests, they must be registered with the server:
```csharp
server.RegisterHandler("index.html", new MyCustomRequestHandler());
```
In this case, we've bound our custom handler to requests for index.html. Note, this is only requests for index.html on the root directory; If something like somefolder/index.html is requested, this handler won't receive the request.

### Wildcard
The registry system supports the use of the * wildcard when registering handlers. There are some limitations to this though. You can only have a single wildcard and it must be the last character of the request. If multiple wildcard characters appear in the designated path, all but the last occurence of the character will be treated literally.

In cases where multiple registered handlers match the requested URL, the handler that was registered first takes precedence.
```chsarp
server.RegisterHandler("mydirectory/*", new MyCustomRequestHandler()); //Intercepts all requests in "mydirectory"
server.RegisterHandler("*", new MyCustomRequestHandler()); //Intercepts all requests, excluding those in "mydirectory"
```
### Default Handlers
In cases where no handler can be found, the registry system defaults to the built-in FileHandler. This can be changed, however, by calling the RegisterDefaultHandler() method:
```csharp
server.RegisterHandler(new MyCustomRequestHandler());
```
