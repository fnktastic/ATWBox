using ATWService.DataAccess;
using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Repository
{
    public class ReadRepository : IReadRepository
    {
        private readonly Context _context;

        public ReadRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Read> Reads => _context
            .Reads
            .AsNoTracking()
            .ToList();

        public async Task SaveReadAsync(Read read)
        {
            if (read != null)
            {
                if (read.ID == Guid.Empty)
                {
                    read.ID = Guid.NewGuid();
                }

                _context.Reads.Add(read);
                await _context.SaveChangesAsync();
            }
        }
    }
}
