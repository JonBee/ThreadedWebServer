using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JonBee.ThreadedWebServer
{
    public class WebServerResponse
    {
        public ContentType ContentType;
        public byte[] Data;

        public WebServerResponse(ContentType contentType, byte[] data)
        {
            Data = data;
            ContentType = contentType;
        }

        public WebServerResponse(string html)
        {
            ContentType = ContentType.HTML;
            Data = Encoding.UTF8.GetBytes(html);
        }
    }
}
