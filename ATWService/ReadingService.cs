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
        private readonly Context _context;
        private readonly IReadRepository _readRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly IReadingRepository _readingRepository;

        static ReadingService()
        {
            Logger.InitLogger();
            Logger.Log.Info("Server started...");
        }

        public ReadingService()
        {
            _context = new Context();
            _readRepository = new ReadRepository(_context);
            _readingRepository = new ReadingRepository(_context);
            _readerRepository = new ReaderRepository(_context);
        }

        public static void Configure(ServiceConfiguration configuration)
        {
            configuration.LoadFromConfiguration();
            Database.SetInitializer(new Initializer());
        }

        #region set
        public async Task<Read> SetRead(Read read)
        {
            try
            {
                if (read == null)
                {
                    throw new ArgumentNullException("Read");
                }

                await Task.WhenAll(_readRepository.SaveReadAsync(read));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return read;
        }

        public async Task<Reading> SetReading(Reading reading)
        {
            if (reading == null)
            {
                throw new ArgumentNullException("Reading");
            }

            await _readingRepository.SaveReading(reading);
            return reading;
        }

        public async Task<Reader> SetReader(Reader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("Reader");
            }

            await _readerRepository.SaveReader(reader);
            return reader;
        }
        #endregion

        #region get
        public Reader GetReaderByID(int readerID)
        {
            return _readerRepository.Readers.FirstOrDefault(x => x.ID == readerID);
        }

        public IEnumerable<Read> GetAllReadsByReadingID(Guid readingID)
        {
            return _readRepository.Reads.Where(x => x.ReadingID == readingID);
        }

        public IEnumerable<Read> GetAllReads()
        {
            return _readRepository.Reads;
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            return _readerRepository.Readers;
        }

        public Read GetReadByID(Guid readID)
        {
            return _readRepository.Reads.FirstOrDefault(x => x.ID == readID);
        }

        public Reading GetReadingByID(Guid readingID)
        {
            return _readingRepository.Readings.FirstOrDefault(x => x.ID == readingID);
        }

        public IEnumerable<Reading> GetAllReadings()
        {
            return _readingRepository.Readings;
        }

        public IEnumerable<Race> GetAllRaces()
        {
            var races = _readingRepository.Readings.ToList().Select(item => new Race()
            {
                Reads = _readRepository.Reads.Where(x => x.ReadingID == item.ID).ToList(),
                Reader = _readerRepository.Readers.FirstOrDefault(x => x.ID == item.ReaderID),
                Reading = item
            }).ToList();

            Logger.Log.Info(nameof(GetAllRaces));
            return races;
        }

        public Race GetRaceByReadingID(Guid readingID)
        {
            var reading = _readingRepository
                .Readings
                .FirstOrDefault(x => x.ID == readingID);

            if (reading != null)
            {
                var reads = _readRepository.Reads.Where(x => x.ReadingID == readingID).ToList();
                var reader = _readerRepository.Readers.FirstOrDefault(x => x.ID == reading.ReaderID);

                return new Race()
                {
                    Reader = reader,
                    Reading = reading,
                    Reads = reads
                };
            }

            return null;
        }
        #endregion
    }
}
