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
        IEnumerable<Reading> Readings { get; }

        Task SaveReading(Reading reading);

        Reading GetById(Guid id);
    }
}
