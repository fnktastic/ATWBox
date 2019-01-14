using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWBox
{
    public static class Consts
    {
        public const int DELAY = 2761;
        private const string HTTP_SERVICE_PATH = "Design_Time_Addresses/ATWService/ReadingService/http";
        private const string TCP_SERVICE_PATH = "Design_Time_Addresses/ATWService/ReadingService/tcp";

        public static string HttpUrl(int port = 8733, string host = "localhost")
        {
            return string.Format("http://{0}:{1}/{2}", host, port, HTTP_SERVICE_PATH);
        }

        public static string TcpUrl(int port = 8732, string host = "127.0.0.1")
        {
            return string.Format("net.tcp://{0}:{1}/{2}", host, port, TCP_SERVICE_PATH);
        }
    }
}
