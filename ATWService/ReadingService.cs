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
            Logger.Log.Info("Server starting...");
            Logger.InitLogger();
        }

        public ReadingService()
        {
            Logger.Log.Info("Incoming request...");
            _context = new Context();
            _readRepository = new ReadRepository(_context);
            _readingRepository = new ReadingRepository(_context);
            _readerRepository = new ReaderRepository(_context);
        }

        public static void Configure(ServiceConfiguration configuration)
        {
            Logger.Log.Info("Configure...");
            configuration.LoadFromConfiguration();
            Database.SetInitializer(new Initializer());
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
        public Reader GetReaderById(int readerId)
        {
            return _readerRepository.Readers.FirstOrDefault(x => x.Id == readerId);
        }

        public IEnumerable<Read> GetAllReadsByReadingId(Guid readingId)
        {
            return _readRepository.Reads.Where(x => x.ReadingId == readingId);
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

        public Read GetReadById(Guid readId)
        {
            return _readRepository.Reads.FirstOrDefault(x => x.Id == readId);
        }

        public async Task<Reading> GetReadingByIdAsync(Guid readingId)
        {
            return (await _readingRepository.ReadingsAsync()).FirstOrDefault(x => x.Id == readingId);
        }

        public async Task <IEnumerable<Reading>> GetAllReadingsAsync()
        {
            Logger.Log.Info("Getting All Readings");
            try
            {
                return await _readingRepository.ReadingsAsync();
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Race>> GetAllRacesAsync()
        {
            var races = (await _readingRepository.ReadingsAsync()).ToList().Select(item => new Race()
            {
                Reads = _readRepository.Reads.Where(x => x.ReadingId == item.Id).ToList(),
                Reader = _readerRepository.Readers.FirstOrDefault(x => x.Id == item.ReaderId),
                Reading = item
            }).ToList();

            Logger.Log.Info(nameof(GetAllRacesAsync));
            return races;
        }

        public async Task<Race> GetRaceByReadingIdAsync(Guid readingId)
        {
            var reading = (await _readingRepository
                .ReadingsAsync())
                .FirstOrDefault(x => x.Id == readingId);

            if (reading != null)
            {
                var reads = _readRepository.Reads.Where(x => x.ReadingId == readingId).ToList();
                var reader = _readerRepository.Readers.FirstOrDefault(x => x.Id == reading.ReaderId);

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
