using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;

namespace JonBee.ThreadedWebServer
{
    /// <summary>
    /// A simple server that asynchronously responds to web requests
    /// </summary>
    public class ThreadedWebServer
    {
        private readonly HttpListener listener = new HttpListener();

        private RequestHandler defaultHandler = new RequestHandler();

        private Dictionary<string, RequestHandler> handlers = new Dictionary<string, RequestHandler>();

        /// <summary>
        /// ThreadedWebServer constructor
        /// </summary>
        /// <param name="prefixes">List of URI prefixes to bind the webserver to. Ex: http://localhost:80/ </param>
        public ThreadedWebServer(List<string> prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                throw new PlatformNotSupportedException("The current system does not support the HttpListener class");
            }

            if (prefixes == null || prefixes.Count == 0)
            {
                throw new ArgumentException("At least one URI prefix must be declared.");
            }

            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
        }

        /// <summary>
        /// Instuct the Webserver to start listening for requests
        /// </summary>
        public void Run()
        {
            listener.Start();
            try
            {
                while (listener.IsListening)
                {
                    ThreadPool.QueueUserWorkItem((c) =>
                    {
                        HttpListenerContext context = c as HttpListenerContext;
                        try
                        {
                            HandleResponse(context.Request, context.Response);
                        }
                        catch { }
                        finally
                        {
                            context.Response.OutputStream.Close();
                        }
                    }, listener.GetContext());
                }
            }
            catch { }
        }

        /// <summary>
        /// Instruct the webserver to stop listening
        /// </summary>
        public void Stop()
        {
            listener.Stop();
            listener.Close();
        }

        private void HandleResponse(HttpListenerRequest request, HttpListenerResponse response)
        {
            //Determine the best handler
            RequestHandler handler = defaultHandler;
            string requestPath = request.Url.LocalPath.ToLower();
            foreach(string key in handlers.Keys)
            {
                //If a wildcard char is used, check if the request starts with key (minus wildcard)
                if (key.EndsWith("*"))
                {
                    if(requestPath.StartsWith(key.TrimEnd('*')))
                    {
                        handler = handlers[key];
                        break;
                    }
                }
                else
                {
                    if (requestPath == key)
                    {
                        handler = handlers[key];
                        break;
                    }
                }
            }

            //Execute HandleRequest method
            WebServerResponse wsresponse = handler.HandleRequest(request);

            //Write response
            response.StatusCode = wsresponse.StatusCode;
            response.ContentType = wsresponse.ContentType.Value;
            response.ContentLength64 = wsresponse.Data.Length;
            response.OutputStream.Write(wsresponse.Data, 0, wsresponse.Data.Length);
        }

        /// <summary>
        /// Registers a handler as the default. If no other handler is found to handle a request, this is used.
        /// </summary>
        /// <param name="handler"></param>
        public void RegisterDefaultHandler(RequestHandler handler)
        {
            defaultHandler = handler;
        }

        /// <summary>
        /// Register a RequestHandler, mapping it to a requested directory or file. Handlers registered first take priority over later registrations.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="handler"></param>
        public void RegisterHandler(string pattern, RequestHandler handler)
        {
            pattern = "/" + pattern.ToLower();
            if (handlers.ContainsKey(pattern))
            {
                handlers[pattern] = handler;
            }
            else
            {
                handlers.Add(pattern, handler);
            }
        }
    }
}
