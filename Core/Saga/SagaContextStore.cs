using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga
{
    public class SagaContextStore
    {
        public SagaContextStore()
        {
            ContextStore = new Dictionary<string, SagaContext>();
        }
        public Dictionary<string, SagaContext> ContextStore { get; set; }

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
            if (sagaContext.IsCompleted || (sagaContext.IsError && sagaContext.IsCanceled) )
                isFinish = true;


            if(isFinish)
                ContextStore.Remove(sagaContext.SagaId);
            return isFinish;
        }
    }
}
