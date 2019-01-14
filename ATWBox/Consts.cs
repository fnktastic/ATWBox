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
        public static string SERVICE_URL = string.Format("http://{0}:{1}/{2}", HOST, PORT, SERVICE);
        public const string HOST = "localhost";
        public const int PORT = 8733;
        public const string SERVICE = "Design_Time_Addresses/ATWService/ReadingService/";
    }
}
