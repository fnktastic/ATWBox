using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Model
{
    [DataContract]
    public class LastSeenLog
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Guid ReadingId { get; set; }

        [DataMember]
        public DateTime LastSeenAt { get; set; }
    }
}
