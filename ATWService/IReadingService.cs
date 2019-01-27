using ATWService.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ATWService
{
    [ServiceContract]
    public interface IReadingService
    {
        [OperationContract]
        Task<Read> SetRead(Read read);

        [OperationContract]
        Task<Reading> SetReading(Reading reading);

        [OperationContract]
        Task<Reader> SetReader(Reader reader);

        [OperationContract]
        Reading GetReadingById(Guid readingID);

        [OperationContract]
        IEnumerable<Reading> GetReadings();
    }
}
