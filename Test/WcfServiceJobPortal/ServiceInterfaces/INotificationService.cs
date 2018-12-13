using System.ServiceModel;

namespace WcfServiceJobPortal
{
    [ServiceContract]
    public interface INotificationService
    {

        [OperationContract]
        void Init();
    }


}
