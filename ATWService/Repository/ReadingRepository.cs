using ATWService.DataAccess;
using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ATWService.Repository
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly Context _context;

        public ReadingRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Reading> Readings => _context
            .Readings
            //.Include(x => x.Reads)
            //.Include(y => y.Reader)
            .AsNoTracking()
            .ToList();

        public async Task SaveReading(Reading reading)
        {
            if (reading != null)
            {
                reading.StartedDateTime = DateTime.UtcNow;

                if (reading.ID == Guid.Empty)
                {
                    reading.ID = Guid.NewGuid();
                }

                _context.Readings.Add(reading);
                await _context.SaveChangesAsync();
            }
        }

        public Reading GetById(Guid id)
        {
            var reading = _context.Readings.FirstOrDefault(x => x.ID == id);

            return reading;
        }
    }
}
