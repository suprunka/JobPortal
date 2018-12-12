using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceWebRole2
{
    [ServiceContract]
    public interface INotificationService
    {
        [OperationContract]
        void Init();
    }
}
