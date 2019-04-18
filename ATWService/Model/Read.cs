using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ATWService.Model
{
    [DataContract]
    public class Read
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string EPC { get; set; } = "<unknown>";

        [DataMember]
        public DateTime Time { get; set; } = DateTime.UtcNow;

        [DataMember]
        public string Signal { get; set; } = "<unknown>";

        [DataMember]
        public string AntennaNumber { get; set; } = "<unknown>";

        [DataMember]
        public int SeenCount { get; set; } = 0;

        [DataMember]
        public int Rank { get; set; } = 0;

        [DataMember] 
        public Guid ReadingId { get; set; }

        [DataMember] 
        public virtual Reading Reading { get; set; }
    }
}