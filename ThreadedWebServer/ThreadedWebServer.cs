using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

namespace JonBee.ThreadedWebServer
{
    public class ThreadedWebServer
    {
        private readonly HttpListener listener = new HttpListener();

        private RequestHandler defaultHandler;

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

        public void Stop()
        {
            listener.Stop();
            listener.Close();
        }

        private void HandleResponse(HttpListenerRequest request, HttpListenerResponse response)
        {
            //Determine the best handler

            //Execute HandleRequest method

            //Write response

            //Test
            WebServerResponse wsresponse = defaultHandler.HandleRequest(request);
            response.StatusCode = wsresponse.StatusCode;
            response.ContentType = wsresponse.ContentType.Value;
            response.ContentLength64 = wsresponse.Data.Length;
            response.OutputStream.Write(wsresponse.Data, 0, wsresponse.Data.Length);
        }

        public void RegisterDefaultHandler(RequestHandler handler)
        {
            defaultHandler = handler;
        }
    }
}
