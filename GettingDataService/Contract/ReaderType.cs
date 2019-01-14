using System.Runtime.Serialization;

namespace GettingDataService.Contract
{
    [DataContract]
    public class ReaderType
    {
        private int _id;
        private string _host;
        private string _port;
    }
}