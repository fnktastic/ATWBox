using ATWService.DataAccess;
using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Repository
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly Context _context;

        public ReadingRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<ReadingType> Readings => _context.Readings;

        public async Task SaveReading(ReadingType reading)
        {
            if(reading != null)
            {
                if(reading.ID == 0)
                {
                    _context.Readings.Add(reading);
                }
                else
                {
                    var dbEntry = _context.Readings.FirstOrDefault(x => x.ID == reading.ID);
                    if(dbEntry != null)
                    {
                        dbEntry.TimingPoint = reading.TimingPoint;
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
