using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Repository
{
    public interface IReadRepository
    {
        IEnumerable<ReadType> Reads { get; }

        Task SaveReadAsync(ReadType readType);
    }
}
