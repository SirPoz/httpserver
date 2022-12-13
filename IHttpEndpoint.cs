using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstHttpServer;

namespace MonsterCardTrading.HttpServer
{
    public interface IHttpEndpoint
    {
        void HandleRequest(HttpRequest request, HttpResponse response);
    }
}
