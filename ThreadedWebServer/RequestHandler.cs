using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace JonBee.ThreadedWebServer
{
    public class RequestHandler
    {
        public virtual WebServerResponse HandleRequest(HttpListenerRequest request)
        {
            return new WebServerResponse("<html><head><meta http-equiv=\"content - type\" content=\"text / html; charset = UTF - 8\"><title>Default Reply</title></head><body></body></html>");
        }
    }
}
