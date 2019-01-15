using ATWService.DataAccess;
using ATWService.Model;
using ATWService.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.ServiceModel;

namespace ATWService
{
    public class ReadingService : IReadingService
    {
        private static Context _context;
        private static IReadRepository _readRepository;
        private static IReaderRepository _readerRepository;
        private static IReadingRepository _readingRepository;

        private static List<ReaderType> activeReaders;
        private static List<ReadingType> activeReadings;
        private static List<ReadType> activeReads;
        
        public static void Configure(ServiceConfiguration configuration)
        {
            configuration.LoadFromConfiguration();
            Database.SetInitializer(new Initializer());
            _context = new Context();
            _readRepository = new ReadRepository(_context);
            _readingRepository = new ReadingRepository(_context);
            _readerRepository = new ReaderRepository(_context);
            activeReaders = new List<ReaderType>();
            activeReadings = new List<ReadingType>();
            activeReads = new List<ReadType>();
        }

        public ReadType GetReadUsingDataContract(ReadType read)
        {
            if (read == null)
            {
                throw new ArgumentNullException("Read");
            }

            activeReads.Add(read);
            return read;
        }

        public ReadingType GetReadingUsingDataContract(ReadingType reading)
        {
            if (reading == null)
            {
                throw new ArgumentNullException("Reading");
            }

            _readingRepository.SaveReading(reading);
            activeReadings.Add(reading);
            return reading;
        }

        public ReaderType GetReaderUsingDataContract(ReaderType reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("Reader");
            }

            _readerRepository.SaveReader(reader);
            activeReaders.Add(reader);
            return reader;
        }
    }
}
