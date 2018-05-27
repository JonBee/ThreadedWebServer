using System.Net;

namespace JonBee.ThreadedWebServer
{
    /// <summary>
    /// RequestHandler is the base class used for responding to requests to the ThreadedWebServer
    /// </summary>
    public class RequestHandler
    {
        public virtual WebServerResponse HandleRequest(HttpListenerRequest request)
        {
            return new WebServerResponse("<html><head><meta http-equiv=\"content - type\" content=\"text / html; charset = UTF - 8\"><title>Default Reply</title></head><body></body></html>");
        }
    }
}
