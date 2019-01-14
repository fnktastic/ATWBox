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

            // Some modifications
            read.Time = DateTime.Now;
            read.ID = 1;
            read.PeakRssiInDbm = "-11Dbm";
            read.Epc = "CHIP 4";
            read.ReadingID = 1;
            read.UniqueReadID = Guid.NewGuid().ToString();

            return read;
        }

        public ReadingType GetReadingUsingDataContract(ReadingType reading)
        {
            if(reading == null)
            {
                throw new ArgumentNullException("Reading");
            }

            return reading;
        }

        public ReaderType GetReaderUsingDataContract(ReaderType reader)
        {
            if(reader == null)
            {
                throw new ArgumentNullException("Reader");
            }

            return reader;
        }
    }
}
