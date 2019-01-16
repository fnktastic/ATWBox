using ATWService.Model;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ATWService
{
    [ServiceContract]
    public interface IReadingService
    {
        [OperationContract]
        Task<ReadType> GetReadUsingDataContract(ReadType read);

        [OperationContract]
        Task<ReadingType> GetReadingUsingDataContract(ReadingType read);

        [OperationContract]
        Task<ReaderType> GetReaderUsingDataContract(ReaderType read);

        [OperationContract]
        RaceType GetRaceUsingDataContract(int readingID);
    }
}
