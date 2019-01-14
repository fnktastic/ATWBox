using ATWService.Model;
using System;
using System.ServiceModel;

namespace ATWService
{
    [ServiceContract]
    public interface IReadingService
    {
        [OperationContract]
        ReadType GetReadUsingDataContract(ReadType read);

        [OperationContract]
        ReadingType GetReadingUsingDataContract(ReadingType read);

        [OperationContract]
        ReaderType GetReaderUsingDataContract(ReaderType read);
    }
}
