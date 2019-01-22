﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWBox
{
    public static class Consts
    {
        public const int DELAY = 2761;
        private const string LOCALHOST_SERVICE_PATH = "ATWService/";
        private const string HTTP_SERVICE_PATH = "ATWService/ReadingService.svc";
        private const string TCP_SERVICE_PATH = "Design_Time_Addresses/ATWService/ReadingService/tcp";

        public static string HttpUrl(int port = 81, string host = "77.68.12.158")
        {
            return string.Format("http://{0}/{2}", host, port, HTTP_SERVICE_PATH);
        }

        public static string HttpLocalhost(int port = 81, string host = "localhost")
        {
            return string.Format("http://{0}/{2}", host, port, LOCALHOST_SERVICE_PATH);
        }

        public static string TcpUrl(int port = 8732, string host = "192.168.1.101")
        {
            return string.Format("net.tcp://{0}:{1}/{2}", host, port, TCP_SERVICE_PATH);
        }
    }
}
