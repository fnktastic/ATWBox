using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Model
{
    [DataContract]
    public class RaceType
    {
        [DataMember]
        public ReaderType Reader { get; set; }

        [DataMember]
        public ReadingType Reading { get; set; }

        [DataMember]
        public IEnumerable<ReadType> Reads { get; set; }
    }
}
