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
        Task<Read> SetReadAsync(Read read);

        [OperationContract]
        Task<Reading> SetReadingAsync(Reading reading);

        [OperationContract]
        Task<Reader> SetReaderAsync(Reader reader);

        [OperationContract]
        Task<Reading> GetReadingByIDAsync(Guid readingID);

        [OperationContract]
        Task<IEnumerable<Reading>> GetAllReadingsAsync();

        [OperationContract]
        Task<IEnumerable<Race>> GetAllRacesAsync();

        [OperationContract]
        Task<Race> GetRaceByReadingIDAsync(Guid readingID);

        [OperationContract]
        Read GetReadByIDAsync(Guid readID);

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
