using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Repository
{
    public interface IReaderRepository
    {
        IEnumerable<ReaderType> Readers { get; }

        Task SaveReader(ReaderType reader);
    }
}
