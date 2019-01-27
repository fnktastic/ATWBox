using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Model
{
    [DataContract]
    public class Race
    {
        [DataMember]
        public Reader Reader { get; set; }

        [DataMember]
        public Reading Reading { get; set; }

        [DataMember]
        public List<Read> Reads { get; set; }
    }
}
