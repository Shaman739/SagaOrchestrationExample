using Saga;
using System;
using System.Collections.Concurrent;

namespace MicroserviceGateway
{
    public class StoreComplitedSaga : IStoreComplitedSaga
    {
        public ConcurrentBag<(string sagaId, int countSuccessSteps, int countNotExecutedSteps, bool isSuccess)> DB { get; private set; }
        public StoreComplitedSaga()
        {
            DB = new ConcurrentBag<(string sagaId, int countSuccessSteps, int countNotExecutedSteps, bool isSuccess)>();
        }
        public void AddCompletedSaga(string sagaId, int countSuccessSteps, int countNotExecutedSteps, bool isSuccess)
        {
            DB.Add((sagaId, countSuccessSteps, countNotExecutedSteps, isSuccess));
        }
    }
}