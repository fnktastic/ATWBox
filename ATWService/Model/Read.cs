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
        public Guid ID { get; set; }

        [DataMember]
        public string EPC { get; set; } = "<unknown>";

        [DataMember]
        public DateTime Time { get; set; } = DateTime.UtcNow;

        [DataMember]
        public string Signal { get; set; } = "<unknown>";

        [DataMember] // relation
        public Guid ReadingID { get; set; }
        [DataMember] // relation
        public Reading Reading { get; set; }
    }
}