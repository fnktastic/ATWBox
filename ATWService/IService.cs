using ATWService.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ATWService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Task<Read> SetReadAsync(Read read);

        [OperationContract]
        Task<Reading> SetReadingAsync(Reading reading);

        [OperationContract]
        Task<Reader> SetReaderAsync(Reader reader);

        [OperationContract]
        Task<Reading> GetReadingByIdAsync(Guid readingId);

        [OperationContract]
        Task<IEnumerable<Reading>> GetAllReadingsAsync();

        [OperationContract]
        Task<IEnumerable<Race>> GetAllRacesAsync();

        [OperationContract]
        Task<Race> GetRaceByReadingIdAsync(Guid readingId);

        [OperationContract]
        Read GetReadById(Guid readId);

        [OperationContract]
        IEnumerable<Read> GetAllReads();

        [OperationContract]
        Reader GetReaderById(int readerId);

        [OperationContract]
        IEnumerable<Reader> GetAllReaders();

        [OperationContract]
        IEnumerable<Read> GetAllReadsByReadingId(Guid readingId);
    }
}
