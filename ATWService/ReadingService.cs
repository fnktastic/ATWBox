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
            Logger.Log.Info("Incoming request...");
        }

        public static void Configure(ServiceConfiguration configuration)
        {
            configuration.LoadFromConfiguration();
            Database.SetInitializer(new Initializer());
            Logger.Log.Info("Configure...");
        }

        #region set
        public async Task<Read> SetReadAsync(Read read)
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

        public async Task<Reading> SetReadingAsync(Reading reading)
        {
            if (reading == null)
            {
                throw new ArgumentNullException("Reading");
            }

            await _readingRepository.SaveReading(reading);
            return reading;
        }

        public async Task<Reader> SetReaderAsync(Reader reader)
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
            Logger.Log.Info("Getting All Readers");
            return _readerRepository.Readers;
        }

        public Read GetReadByIDAsync(Guid readID)
        {
            return _readRepository.Reads.FirstOrDefault(x => x.ID == readID);
        }

        public async Task<Reading> GetReadingByIDAsync(Guid readingID)
        {
            return (await _readingRepository.ReadingsAsync()).FirstOrDefault(x => x.ID == readingID);
        }

        public async Task <IEnumerable<Reading>> GetAllReadingsAsync()
        {
            Logger.Log.Info("Getting All Readings");
            return await _readingRepository.ReadingsAsync();
        }

        public async Task<IEnumerable<Race>> GetAllRacesAsync()
        {
            var races = (await _readingRepository.ReadingsAsync()).ToList().Select(item => new Race()
            {
                Reads = _readRepository.Reads.Where(x => x.ReadingID == item.ID).ToList(),
                Reader = _readerRepository.Readers.FirstOrDefault(x => x.ID == item.ReaderID),
                Reading = item
            }).ToList();

            Logger.Log.Info(nameof(GetAllRacesAsync));
            return races;
        }

        public async Task<Race> GetRaceByReadingIDAsync(Guid readingID)
        {
            var reading = (await _readingRepository
                .ReadingsAsync())
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
