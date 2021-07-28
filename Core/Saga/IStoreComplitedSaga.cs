using System.Collections.Concurrent;

namespace Saga
{
    public interface IStoreComplitedSaga
    {
        ConcurrentBag<(string sagaId, int countSuccessSteps, int countNotExecutedSteps, bool isSuccess)> DB { get; }

        void AddCompletedSaga(string sagaId, int countSuccessSteps, int countNotExecutedSteps, bool isSuccess);
    }
}