using GettingDataService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GettingDataService
{
    [ServiceContract]
    public interface IReadingService
    {
        [OperationContract]
        ReadType GetReadUsingDataContract(ReadType read);
    }
}
