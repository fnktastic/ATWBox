using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ATWService.Model
{
    [DataContract]
    public class ReadType
    {
        private int _id = -1;
        private string _epc = "<unknown>";
        private DateTime _time = DateTime.UtcNow;
        private string _peakRssiInDbm = "<unknown>";
        private string _uniqueReadID = "<unknown>";

        private int _readingID;

        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public string Epc
        {
            get { return _epc; }
            set { _epc = value; }
        }

        [DataMember]
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        [DataMember]
        public string PeakRssiInDbm
        {
            get { return _peakRssiInDbm; }
            set { _peakRssiInDbm = value; }
        }

        [DataMember]
        public int ReadingID
        {
            get { return _readingID; }
            set { _readingID = value; }
        }

        [DataMember]
        public string UniqueReadID
        {
            get { return _uniqueReadID; }
            set { _uniqueReadID = value; }
        }
    }
}