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
    public interface IReadService
    {
        Task AddOrUpdateAsync(Read read);
        IEnumerable<Read> GetAllByReadingId(Guid readingId);
        IEnumerable<Read> GetAll();
        Read GetById(Guid readId);
    }

    public class ReadService : IReadService
    {
        private readonly Context _context;
        private readonly IReadRepository _readRepository;

        public ReadService(Context context)
        {
            _context = context;
            _readRepository = new ReadRepository(_context);
        }

        public async Task AddOrUpdateAsync(Read read)
        {
            await _readRepository.SaveReadAsync(read);
        }

        public IEnumerable<Read> GetAllByReadingId(Guid readingId)
        {
            return _readRepository.Reads.Where(x => x.ReadingId == readingId).ToList();
        }

        public IEnumerable<Read> GetAll()
        {
            return _readRepository.Reads.ToList();
        }

        public Read GetById(Guid readId)
        {
            return _readRepository.Reads.FirstOrDefault(x => x.Id == readId);
        }
    }
}
