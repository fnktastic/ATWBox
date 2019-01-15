using ATWService.DataAccess;
using ATWService.Model;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.ServiceModel;

namespace ATWService
{
    public class ReadingService : IReadingService
    {

        public static void Configure(ServiceConfiguration configuration)
        {
            configuration.LoadFromConfiguration();
            Database.SetInitializer(new Initializer());
        }

        public ReadType GetReadUsingDataContract(ReadType read)
        {
            if (read == null)
            {
                throw new ArgumentNullException("Read");
            }

            return read;
        }

        public ReadingType GetReadingUsingDataContract(ReadingType reading)
        {
            if (reading == null)
            {
                throw new ArgumentNullException("Reading");
            }

            return reading;
        }

        public ReaderType GetReaderUsingDataContract(ReaderType reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("Reader");
            }

            return reader;
        }
    }
}
