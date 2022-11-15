using System.ServiceModel;

namespace PumpService
{
    [ServiceContract]
    public interface IPumpServiceCallback
    {
        [OperationContract]
        void UpdateStatistics(IStatisticsService statistics);
    }
}