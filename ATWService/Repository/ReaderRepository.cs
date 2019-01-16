using ATWService.DataAccess;
using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Repository
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly Context _context;

        public ReaderRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<ReaderType> Readers => _context.Readers;

        public async Task SaveReader(ReaderType reader)
        {
            if(reader != null)
            {
                if(reader.ID == 0)
                {
                    _context.Readers.Add(reader);
                }
                else
                {
                    var dbEntry = _context.Readers.FirstOrDefault(x => x.ID == reader.ID);

                    dbEntry.Host = reader.Host;
                    dbEntry.Port = reader.Port;
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
