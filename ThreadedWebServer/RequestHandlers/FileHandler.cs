using System.Net;
using System.IO;

namespace JonBee.ThreadedWebServer
{
    /// <summary>
    /// The FileHandler is a basic RequestHandler that will attempt to serve up files and directory listings automatically
    /// </summary>
    public class FileHandler : RequestHandler
    {
        //This variable controls the ability of the FileHandler to return the contents of a directory. If false, it returns a 403, forbidden.
        bool allowDirectoryListings = false;

        public FileHandler(bool showFileLists)
        {
            allowDirectoryListings = showFileLists;
        }

        public override WebServerResponse HandleRequest(HttpListenerRequest request)
        {
            WebServerResponse response;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), request.Url.LocalPath.TrimStart('/'));
            if (Directory.Exists(filePath))
            {
                if(allowDirectoryListings)
                {
                    string directoryName = Path.GetFileName(filePath);
                    string html = "<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset = UTF-8\"></head><title>Contents of " + directoryName + "</title><body>";
                    html += "<h3>Directory Listing</h3><h4>" + request.Url.LocalPath.TrimStart('/') + "</h4>";


                    //List all folders
                    html += "<p>Directories</p><ul>";
                    foreach (string dir in Directory.GetDirectories(filePath))
                    {
                        string dirName = Path.GetFileName(dir);
                        html += "<li><a href=\"" + Path.Combine(request.Url.LocalPath.TrimStart('/'), dirName) + "\">" + dirName + "</a></li>";
                    }
                    html += "</ul>";

                    //List all files
                    html += "<p>Files</p><ul>";
                    foreach (string file in Directory.GetFiles(filePath))
                    {
                        string fileName = Path.GetFileName(file);
                        html += "<li><a href=\"./" + directoryName + "/" + fileName + "\">" + fileName + "</a></li>";
                    }
                    html += "</ul>";

                    html += "</body></html>";
                    response = new WebServerResponse(html);
                }
                else
                {
                    //Handler has disallowed listing directory entries. Return a 403 instead
                    response = new WebServerResponse("<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset = UTF-8\"></head><body><p>Forbidden.</p></body></html>") { StatusCode = 403 };
                }
            }
            else if (File.Exists(filePath))
            {
                //File exists, attempt to return that file with the correct content-type
                response = WebServerResponse.FromFile(filePath);
            }else
            {
                //File or directory does not exist, return a 404
                response = new WebServerResponse("<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset = UTF-8\"></head><body><p>Unable to find the specified file.</p></body></html>") { StatusCode = 404 };
            }

            return response;
        }
    }
}
