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

        public IEnumerable<ReadType> Reads => _context.Reads;

        public async Task SaveReadAsync(ReadType read)
        {
            if (read != null)
            {
                if (read.ID == 0)
                    _context.Reads.Add(read);
                else
                {
                    var dbEntry = _context.Reads.FirstOrDefault(x => x.ID == read.ID);
                    if (dbEntry != null)
                    {
                        dbEntry.Epc = read.Epc;
                        dbEntry.PeakRssiInDbm = read.PeakRssiInDbm;
                        dbEntry.Time = read.Time;
                        dbEntry.UniqueReadID = read.UniqueReadID;
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
