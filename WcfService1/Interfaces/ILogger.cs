using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Checkers.Interfaces
{
    
    [ServiceContract]
    public interface ILogger
    {
        [OperationContract]
        void DoWork();
    }
}
