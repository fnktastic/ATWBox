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
        Task AddOrUpdateRangeAsync(IEnumerable<Reading> readings);
        Task<Reading> GetByIdAsync(Guid readingId);
        Task<IEnumerable<Reading>> GetAllAsync();
        Task RecalculateAllReadCount();
        Task<IEnumerable<Reading>> GetByIds(IEnumerable<Guid> readingIds);
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
            await _readingRepository.SaveReadingAsync(reading);
        }

        public async Task AddOrUpdateRangeAsync(IEnumerable<Reading> readings)
        {
            await _readingRepository.SaveReadingsRangeAsync(readings);
        }

        public async Task<Reading> GetByIdAsync(Guid readingId)
        {
            return (await _readingRepository.ReadingsAsync()).FirstOrDefault(x => x.Id == readingId);
        }

        public async Task<IEnumerable<Reading>> GetAllAsync()
        {
            return await _readingRepository.ReadingsAsync();
        }

        public async Task RecalculateAllReadCount()
        {
            var all = await _readingRepository.ReadingsAsync();

            foreach(var item in all)
            {
                item.TotalReads = item.Reads.Count();
            }

            await AddOrUpdateRangeAsync(all);
        }

        public async Task<IEnumerable<Reading>> GetByIds(IEnumerable<Guid> readingIds)
        {
            return (await _readingRepository.ReadingsAsync())
                .Join(readingIds, o => o.Id, id => id, (o, id) => o);
        }
    }
}
