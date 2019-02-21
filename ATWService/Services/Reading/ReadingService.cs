using ATWService.DataAccess;
using ATWService.Model;
using ATWService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Services
{
    public interface IReadingService
    {
        Task AddOrUpdateAsync(Reading reading);
        Task<Reading> GetByIdAsync(Guid readingId);
        Task<IEnumerable<Reading>> GetAllAsync();
    }

    public class ReadingService : IReadingService
    {
        private readonly Context _context;
        private readonly IReadingRepository _readingRepository;

        public ReadingService(Context context)
        {
            _context = context;
            _readingRepository = new ReadingRepository(_context);
        }

        public async Task AddOrUpdateAsync(Reading reading)
        {
            await _readingRepository.SaveReading(reading);
        }

        public async Task<Reading> GetByIdAsync(Guid readingId)
        {
            return (await _readingRepository.ReadingsAsync()).FirstOrDefault(x => x.Id == readingId);
        }

        public async Task<IEnumerable<Reading>> GetAllAsync()
        {
            return await _readingRepository.ReadingsAsync();
        }
    }
}
