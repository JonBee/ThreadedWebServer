namespace JonBee.ThreadedWebServer
{
    /// <summary>
    /// A container class for handling MIME types
    /// </summary>
    public class ContentType
    {
        private string cType;
        public string Value
        {
            get
            {
                return cType;
            }
        }

        /// <summary>
        /// Initializes an instance of the ContentType class
        /// </summary>
        /// <param name="val">The MIME type of the data; EX: text/html</param>
        public ContentType(string val)
        {
            cType = val;
        }

        public static ContentType HTML
        {
            get
            {
                return new ContentType("text/html");
            }
        }

        public static ContentType PlainText
        {
            get
            {
                return new ContentType("text/plain");
            }
        }

        public static ContentType JPEG
        {
            get
            {
                return new ContentType("image/jpeg");
            }
        }

        public static ContentType PNG
        {
            get
            {
                return new ContentType("image/png");
            }
        }

        public static ContentType MPEG
        {
            get
            {
                return new ContentType("audio/mpeg");
            }
        }

        public static ContentType OGG
        {
            get
            {
                return new ContentType("audio/ogg");
            }
        }

        public static ContentType MP4
        {
            get
            {
                return new ContentType("video/mp4");
            }
        }

        public static ContentType JSON
        {
            get
            {
                return new ContentType("application/json");
            }
        }

        public static ContentType Javascript
        {
            get
            {
                return new ContentType("application/javascript");
            }
        }

        public static ContentType ECMAScript
        {
            get
            {
                return new ContentType("application/ecmascript");
            }
        }

        public static ContentType UnknownBinaryFormat
        {
            get
            {
                return new ContentType("application/octet-stream");
            }
        }

        public static ContentType MultipartFormData
        {
            get
            {
                return new ContentType("multipart/form-data");
            }
        }

        public static ContentType SVG
        {
            get
            {
                return new ContentType("image/svg+xml");
            }
        }

        public static ContentType GIF
        {
            get
            {
                return new ContentType("image/gif");
            }
        }

        public static ContentType CSS
        {
            get
            {
                return new ContentType("text/css");
            }
        }
    }
}
