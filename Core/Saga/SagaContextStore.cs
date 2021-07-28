using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga
{
    public class SagaContextStore
    {
        public SagaContextStore(IStoreComplitedSaga storeComplitedSaga)
        {
            ContextStore = new Dictionary<string, SagaContext>();
            _storeComplitedSaga = storeComplitedSaga;
        }
        public Dictionary<string, SagaContext> ContextStore { get; set; }

        private IStoreComplitedSaga _storeComplitedSaga;

        public void ContinueSaga(string sagaId, bool isCanceled)
        {
            ContextStore.TryGetValue(sagaId, out SagaContext sagaContext);
            if (sagaContext == null)
                return;

            if (!isCanceled)
                sagaContext.SuccessCurrentStep();
           
            if (!FinishSaga(sagaContext))
                sagaContext.Execute();
        }

        public void CancelSaga(string sagaId)
        {

            ContextStore.TryGetValue(sagaId, out SagaContext sagaContext);
            sagaContext.IsError = true;
          
            if(!FinishSaga(sagaContext))
             sagaContext.Execute();
        }

        private bool FinishSaga(SagaContext sagaContext)
        {
            bool isFinish = false;
            if (sagaContext.IsCompleted || (sagaContext.IsError && sagaContext.IsCanceled))
            {
                isFinish = true;
                _storeComplitedSaga.AddCompletedSaga(sagaId: sagaContext.SagaId, countSuccessSteps: sagaContext.CompletedSteps.Count, countNotExecutedSteps: sagaContext.NextSteps.Count, isSuccess: !sagaContext.IsError);
            }


            if(isFinish)
                ContextStore.Remove(sagaContext.SagaId);
            return isFinish;
        }
    }
}
