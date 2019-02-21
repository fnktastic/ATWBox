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
    public interface IRaceService
    {
        Task<IEnumerable<Race>> GetAllAsync();
        Task<Race> GetRaceByReadingIdAsync(Guid readingId);
    }

    public class RaceService : IRaceService
    {
        private readonly Context _context;
        private readonly IReadRepository _readRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly IReadingRepository _readingRepository;

        public RaceService(Context context)
        {
            _context = context;
            _readRepository = new ReadRepository(_context);
            _readerRepository = new ReaderRepository(_context);
            _readingRepository = new ReadingRepository(_context);
        }

        public async Task<IEnumerable<Race>> GetAllAsync()
        {
            var races = (await _readingRepository.ReadingsAsync()).ToList().Select(item => new Race()
            {
                Reads = _readRepository.Reads.Where(x => x.ReadingId == item.Id).ToList(),
                Reader = _readerRepository.Readers.FirstOrDefault(x => x.Id == item.ReaderId),
                Reading = item
            }).ToList();

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
    }
}
