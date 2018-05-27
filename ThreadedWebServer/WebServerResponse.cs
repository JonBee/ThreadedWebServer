using System.Text;
using System.IO;

namespace JonBee.ThreadedWebServer
{
    /// <summary>
    /// WebServerResponse contains the data required to serve up a complete response to a server request
    /// </summary>
    public class WebServerResponse
    {
        public ContentType ContentType;
        public byte[] Data;

        /// <summary>
        /// Response status code for the request; 200 = OK (default), 403 = FORBIDDEN, 404 = FILE_NOT_FOUND, etc.
        /// </summary>
        public int StatusCode = 200;

        /// <summary>
        /// Create a new WebServerResponse instance and set it's content-type and binary data
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="data"></param>
        public WebServerResponse(ContentType contentType, byte[] data)
        {
            Data = data;
            ContentType = contentType;
        }

        /// <summary>
        /// Create a new WebServerResponse instance and set it's content-type to ContentType.HTML and set it's Data as a UTF8 string
        /// </summary>
        /// <param name="html"></param>
        public WebServerResponse(string html)
        {
            ContentType = ContentType.HTML;
            Data = Encoding.UTF8.GetBytes(html);
        }

        /// <summary>
        /// Attempts to determine the MIME type of the file and generates the appropriate response from that.
        /// </summary>
        /// <param name="file">Path to the desired file</param>
        /// <returns></returns>
        public static WebServerResponse FromFile(string file)
        {
            string extension = Path.GetExtension(file).TrimStart('.').ToLower();

            ContentType detectedType = ContentType.UnknownBinaryFormat;

            switch(extension)
            {
                case "jpg":
                case "jpeg":
                    detectedType = ContentType.JPEG;
                    break;
                case "png":
                    detectedType = ContentType.PNG;
                    break;
                case "html":
                case "htm":
                    detectedType = ContentType.HTML;
                    break;
                case "css":
                    detectedType = ContentType.CSS;
                    break;
                case "gif":
                    detectedType = ContentType.GIF;
                    break;
                case "mp3":
                    detectedType = ContentType.MPEG;
                    break;
                case "mp4":
                    detectedType = ContentType.MP4;
                    break;
                case "txt":
                    detectedType = ContentType.PlainText;
                    break;
                case "json":
                    detectedType = ContentType.JSON;
                    break;
                case "js":
                    detectedType = ContentType.Javascript;
                    break;
                case "svg":
                    detectedType = ContentType.SVG;
                    break;
                case "ogg":
                    detectedType = ContentType.OGG;
                    break;
            }

            return new WebServerResponse(detectedType, File.ReadAllBytes(file));
        }
    }
}
