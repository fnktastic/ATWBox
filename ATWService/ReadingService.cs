using ATWService.DataAccess;
using ATWService.Model;
using ATWService.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

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

        public async Task<ReadType> GetReadUsingDataContract(ReadType read)
        {
            if (read == null)
            {
                throw new ArgumentNullException("Read");
            }

            await _readRepository.SaveReadAsync(read).ConfigureAwait(false);
            activeReads.Add(read);
            return read;
        }

        public async Task<ReadingType> GetReadingUsingDataContract(ReadingType reading)
        {
            if (reading == null)
            {
                throw new ArgumentNullException("Reading");
            }

            await _readingRepository.SaveReading(reading);
            activeReadings.Add(reading);
            return reading;
        }

        public async Task<ReaderType> GetReaderUsingDataContract(ReaderType reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("Reader");
            }

            await _readerRepository.SaveReader(reader);
            activeReaders.Add(reader);
            return reader;
        }

        public RaceType GetRaceUsingDataContract(int readingID = 1)
        {
            var reading = _readingRepository.Readings.FirstOrDefault(x => x.ID == readingID);
            if (reading != null)
            {
                var reads = _readRepository.Reads.Where(x => x.ReadingID == reading.ID);
                var reader = _readerRepository.Readers.FirstOrDefault(x => x.ID == reading.ReaderID);

                RaceType race = new RaceType()
                {
                    Reader = reader,
                    Reading = reading,
                    Reads = reads
                };

                return race;
            }

            return null;
        }
    }
}
