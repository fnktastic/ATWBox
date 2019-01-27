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

        public Reading GetReadingById(Guid readingID)
        {
            return _readingRepository.GetById(readingID); ;
        }

        public IEnumerable<Reading> GetReadings()
        {
            return _readingRepository.Readings;
        }


    }
}
