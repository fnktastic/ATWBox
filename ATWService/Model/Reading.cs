using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ATWService.Model
{
    [DataContract]
    public class Reading
    {
        [DataMember]
        public Guid ID { get; set; } = new Guid();

        [DataMember]
        public string AntennaNumber { get; set; } = "<unknown>";

        [DataMember]
        public string ReaderNumber { get; set; } = "<unknown>";

        [DataMember]
        public string IPAddress { get; set; } = "<unknown>";

        [DataMember]
        public string TimingPoint { get; set; } = "<unknown>";

        [DataMember]
        public int TotalReads { get; set; } = 0;

        [DataMember]
        public string FileName { get; set; } = "<unknown>";

        [DataMember]
        public DateTime? StartedDateTime { get; set; } = null;

        [DataMember]
        public DateTime? EndedDateTime { get; set; } = null;

        [DataMember] 
        public int ReaderID { get; set; }

        [DataMember]
        public virtual Reader Reader { get; set; }

        [DataMember]
        public virtual ICollection<Read> Reads { get; set; }
    }
}