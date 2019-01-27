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

        public IEnumerable<Reading> Readings => _context.Readings;

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
            return _context.Readings.FirstOrDefault(x => x.ID == id);
        }
    }
}
