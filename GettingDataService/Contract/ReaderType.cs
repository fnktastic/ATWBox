using System.Runtime.Serialization;

namespace GettingDataService.Contract
{
    [DataContract]
    public class ReaderType
    {
        private int _id = -1;
        private string _host = "<unknown>";
        private string _port = "<unknown>";

        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        [DataMember]
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }
    }
}