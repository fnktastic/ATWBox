using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ATWService.Model
{
    [DataContract]
    public class Reader
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Host { get; set; } = "<unknown>";

        [DataMember]
        public string Port { get; set; } = "<unknown>";

        [DataMember] 
        public virtual ICollection<Reading> Readings { get; set; }
    }
}