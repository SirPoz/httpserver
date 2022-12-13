using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FirstHttpServer
{
    public class HttpRequest
    {
        private StreamReader reader;

        public HttpRequest(StreamReader reader)
        {
            this.reader = reader;
        }

        public string Method { get; private set; }
        public string Path { get; private set; }

        public string Content { get; private set; }
        public string ProtocolVersion { get; private set; }

        public Dictionary<string,string> Headers = new ();
        public void Parse()
        {
            string line = reader.ReadLine();
            
            var firstLineParts = line.Split(" ");

            Method          = firstLineParts[0];
            Path            = firstLineParts[1];
            ProtocolVersion = firstLineParts[2];


            //headers
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
                //Console.WriteLine(line);
                if (line.Length == 0)
                {
                    break;
                }

                var headerParts = line.Split(":");
                Headers[headerParts[0]] = headerParts[1];
            }

            
            //content...
            string contentLenghtString = "0";
            Headers.TryGetValue("Content-Length", out contentLenghtString);
            int contentLength;
            StringBuilder bufferContent = new StringBuilder();
            int.TryParse(contentLenghtString, out contentLength);

            if (contentLength > 0)
            {
                char[] buffer = new char[1024];
                int totalbytesRead = 0;
                while (totalbytesRead < contentLength)
                {
                    var bytesRead = reader.Read(buffer, 0, 1024);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    totalbytesRead += bytesRead;
                    bufferContent.Append(buffer,0,bytesRead);
                }
            }
            Content = bufferContent.ToString();
        }
    }
}
