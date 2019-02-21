using ATWService.DataAccess;
using ATWService.Model;
using ATWService.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ATWService
{
    public interface IUpdatable
    {
        void Update(LastSeenLog lastSeenLog);
        void Update(Guid readingId);
    }

    public class Service : IService, IUpdatable
    {
        private readonly Context _context;
        private readonly IReadService _readService;
        private readonly IReaderService _readerService;
        private readonly IReadingService _readingService;
        private readonly IRaceService _raceService;
        private readonly ILastSeenLogService _lastSeenLogService;

        static Service()
        {
            Logger.Log.Info("Server starting...");
            Logger.InitLogger();
        }

        public void Update(LastSeenLog lastSeenLog)
        {
            _lastSeenLogService.AddOrUpdateAsync(lastSeenLog);
        }

        public void Update(Guid readingId)
        {
            var lastSeenLog = new LastSeenLog()
            {
                LastSeenAt = DateTime.UtcNow,
                ReadingId = readingId
            };

            _lastSeenLogService.AddOrUpdateAsync(lastSeenLog);
        }

        public Service()
        {
            Logger.Log.Info("Incoming request...");
            _context = new Context();
            _readService = new ReadService(_context);
            _readerService = new ReaderService(_context);
            _readingService = new ReadingService(_context);
            _raceService = new RaceService(_context);
            _lastSeenLogService = new LastSeenLogService(_context);
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

                await _readService.AddOrUpdateAsync(read);
                
                Update(read.ReadingId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return read;
        }

        public async Task<Reading> SetReadingAsync(Reading reading)
        {
            try
            {
                if (reading == null)
                {
                    throw new ArgumentNullException("Reading");
                }

                await _readingService.AddOrUpdateAsync(reading);

                Update(reading.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return reading;
        }

        public async Task<Reader> SetReaderAsync(Reader reader)
        {
            try
            {
                if (reader == null)
                {
                    throw new ArgumentNullException("Reader");
                }

                await _readerService.AddOrUpdateAsync(reader);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return reader;
        }
        #endregion

        #region get
        public Reader GetReaderById(int readerId)
        {
            return _readerService.GetReaderById(readerId);
        }

        public IEnumerable<Read> GetAllReadsByReadingId(Guid readingId)
        {
            return _readService.GetAllByReadingId(readingId);
        }

        public IEnumerable<Read> GetAllReads()
        {
            return _readService.GetAll();
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            Logger.Log.Info("Getting All Readers");

            return _readerService.GetAll();
        }

        public Read GetReadById(Guid readId)
        {
            return _readService.GetById(readId);
        }

        public async Task<Reading> GetReadingByIdAsync(Guid readingId)
        {
            return await _readingService.GetByIdAsync(readingId);
        }

        public async Task <IEnumerable<Reading>> GetAllReadingsAsync()
        {
            Logger.Log.Info("Getting All Readings");

            try
            {
                return await _readingService.GetAllAsync();
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Race>> GetAllRacesAsync()
        {
            Logger.Log.Info(nameof(GetAllRacesAsync));

            return await _raceService.GetAllAsync();
        }

        public async Task<Race> GetRaceByReadingIdAsync(Guid readingId)
        {
            return await _raceService.GetRaceByReadingIdAsync(readingId);
        }

        public async Task<IEnumerable<LastSeenLog>> GetLastSeenLogsAsync()
        {
            return await _lastSeenLogService.GetAllAsync();
        }

        public async Task<LastSeenLog> GetLastSeenLogByReadingIdAsync(Guid readingId)
        {
            return await _lastSeenLogService.GetByReadingId(readingId);
        }

        public async Task<IEnumerable<LastSeenLog>> GetAllAliveLastSyncLogsAsync()
        {
            return await _lastSeenLogService.GetAllAliveAsync();
        }

        public async Task<IEnumerable<LastSeenLog>> GetAllPastLastSyncLogsAsync()
        {
            return await _lastSeenLogService.GetAllPastAsync();
        }
        #endregion
    }
}
