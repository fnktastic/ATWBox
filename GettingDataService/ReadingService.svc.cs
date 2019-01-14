using GettingDataService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GettingDataService
{
    public class ReadingService : IReadingService
    {
        public ReadType GetReadUsingDataContract(ReadType read)
        {
            if (read == null)
            {
                throw new ArgumentNullException("Read");
            }

            read.Time = DateTime.Now;
            read.ID = 1;
            read.PeakRssiInDbm = "-11Dbm";
            read.Epc = "CHIP 4";
            read.ReadingID = 1;
            
            return read;
        }
    }
}
