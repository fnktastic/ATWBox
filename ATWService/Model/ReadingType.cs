using System;
using System.Runtime.Serialization;

namespace ATWService.Model
{
    [DataContract]
    public class ReadingType
    {
        private int _id = -1;
        private string _antennaNumber = "<unknown>";
        private string _readerNumber = "<unknown>";
        private string _ipAddress = "<unknown>";
        private string _timingPoint = "<unknown>";
        private int _totalReads = 0;
        private string _fileName = "<unknown>";
        private DateTime? _startedDateTime = null;
        private DateTime? _endedDateTime = null;
        private int _readerID = -1;

        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public string AntennaNumber
        {
            get { return _antennaNumber; }
            set { _antennaNumber = value; }
        }

        [DataMember]
        public string ReaderNumber
        {
            get { return _readerNumber; }
            set { _readerNumber = value; }
        }

        [DataMember]
        public string IPAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        [DataMember]
        public string TimingPoint
        {
            get { return _timingPoint; }
            set { _timingPoint = value; }
        }

        [DataMember]
        public int TotalReads
        {
            get { return _totalReads; }
            set { _totalReads = value; }
        }

        [DataMember]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [DataMember]
        public DateTime? StartedDateTime
        {
            get { return _startedDateTime; }
            set { _startedDateTime = value; }
        }

        [DataMember]
        public DateTime? EndedDateTime
        {
            get { return _endedDateTime; }
            set { _endedDateTime = value; }
        }

        [DataMember]
        public  int ReaderID
        {
            get { return _readerID; }
            set { _readerID = value; }
        }
    }
}