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
    public interface IReaderService
    {
        Task AddOrUpdateAsync(Reader reader);
        Reader GetReaderById(int readerId);
        IEnumerable<Reader> GetAll();
    }

    public class ReaderService :IReaderService
    {
        private readonly Context _context;
        private readonly IReaderRepository _readerRepository;

        public ReaderService(Context context)
        {
            _context = context;
            _readerRepository = new ReaderRepository(_context);
        }

        public async Task AddOrUpdateAsync(Reader reader)
        {
            await _readerRepository.SaveReader(reader);
        }

        public Reader GetReaderById(int readerId)
        {
            return _readerRepository.Readers.FirstOrDefault(x => x.Id == readerId);
        }

        public IEnumerable<Reader> GetAll()
        {
            return _readerRepository.Readers.ToList();
        }
    }
}
