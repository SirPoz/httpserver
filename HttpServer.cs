using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MonsterCardTrading.HttpServer;

namespace FirstHttpServer
{
    public class HttpServer
    {
        private TcpListener httpServer = new TcpListener(IPAddress.Loopback, 10001);
        public Dictionary<string, IHttpEndpoint> endpoints { get; private set; }= new Dictionary<string, IHttpEndpoint>();
        
        public void run()
        {

            httpServer.Start();
            while (true)
            {
                var clientSocket = httpServer.AcceptTcpClient();
                var httpProcessor = new HttpProcessor(this, clientSocket);
                Task.Factory.StartNew(() =>
                {
                    httpProcessor.run();
                });
            }
        }

        public void RegisterEndpoint(string path, IHttpEndpoint endpoint)
        {
            endpoints.Add(path,endpoint);
        }
    }
}
