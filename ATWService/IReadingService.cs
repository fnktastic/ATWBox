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
        Reading GetReadingByID(Guid readingID);

        [OperationContract]
        IEnumerable<Reading> GetAllReadings();

        [OperationContract]
        IEnumerable<Race> GetAllRaces();

        [OperationContract]
        Race GetRaceByReadingID(Guid readingID);

        [OperationContract]
        Read GetReadByID(Guid readID);

        [OperationContract]
        IEnumerable<Read> GetAllReads();

        [OperationContract]
        Reader GetReaderByID(int readerID);

        [OperationContract]
        IEnumerable<Reader> GetAllReaders();

        [OperationContract]
        IEnumerable<Read> GetAllReadsByReadingID(Guid readingID);
    }
}
