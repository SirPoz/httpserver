using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MonsterCardTrading.HttpServer;

namespace FirstHttpServer
{
    public class HttpProcessor
    {
        private TcpClient clientSocket;
        private HttpServer server;

        public HttpProcessor(HttpServer server, TcpClient clientSocket)
        {
            this.clientSocket = clientSocket;
            this.server = server;
        }

        public void run()
        {
            var reader = new StreamReader(clientSocket.GetStream());
            var request = new HttpRequest(reader);
            request.Parse();






            var writer = new StreamWriter(clientSocket.GetStream()) { AutoFlush = true };
            var response = new HttpResponse(writer);
            request.Headers.TryGetValue("Content-Length", out var contentLength);
            response.ResponseCode = 200;
            response.ResponseText = "OK";
            response.ResponseContent = "\nMethod: " + request.Method + "\nPath: " + request.Path + "\nContent-Length: " +
                                       contentLength + "\nContent: " + request.Content;
                                       
            response.Process();
        }
    }
}
