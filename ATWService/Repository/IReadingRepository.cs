using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Repository
{
    public interface IReadingRepository
    {
        Task<IEnumerable<Reading>> ReadingsAsync();

        Task SaveReading(Reading reading);

        Reading GetById(Guid Id);
    }
}
