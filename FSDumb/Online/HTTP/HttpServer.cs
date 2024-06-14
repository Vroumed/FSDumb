using nanoFramework.WebServer;
using System.Net;
using Vroumed.FSDumb.Dependencies;

namespace Vroumed.FSDumb.Online.HTTP
{
    public class HttpServer : IDependencyCandidate
    {

        [Resolved]
        private Connector Connector { get; }
        
        private WebServer WebServer { get; }

        public HttpServer()
        {
            WebServer = new WebServer(80, HttpProtocol.Http);
            WebServer.CommandReceived += WebServerOnCommandReceived;
        }

        public void ThreadedStart()
        {
            WebServer.Start();
        }

        private void WebServerOnCommandReceived(object obj, WebServerEventArgs e)
        {
            var url = e.Context.Request.RawUrl;

            if (e.Context.Request.HttpMethod != "GET")
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.MethodNotAllowed);
                return;
            }

            switch (url.ToLower())
            {
                case "/":
                    e.Context.Response.ContentType = "text/text";
                    e.Context.Response.StatusCode = 200;
                    WebServer.OutPutStream(e.Context.Response, "Hello World");
                    break;                
                case "/backendScan":
                    if (Connector.BackendOnline)
                    {
                        e.Context.Response.ContentType = "text/text";
                        e.Context.Response.StatusCode = 503;
                        WebServer.OutPutStream(e.Context.Response, "Rover already linked to backend");
                        return;
                    }

                    Connector.BackendEndPoint = e.Context.Request.RemoteEndPoint;
                    Connector.BackendOnline = true;
                    Connector.StartBackendCommunication();
                    
                    break;
                default:
                    e.Context.Response.StatusCode = 404;
                    break;
            }
        }
    }
}
