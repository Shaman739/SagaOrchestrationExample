using Core.Data.Contract.EventBus;
using Saga.Step;
using System;
using System.Collections.Generic;

namespace Saga
{
    public abstract class Saga<T> where T : SagaParam
    {
        private SagaContextStore sagaContextStore;

        public Saga(SagaContextStore sagaContextStore)
        {
            this.sagaContextStore = sagaContextStore;
            Steps = new List<ISagaStep>();
        }
        public List<ISagaStep> Steps{ get; set; }

        protected void AddStep(ISagaStep step)
        {
            Steps.Add(step);
        }

        public abstract void Create(T param);

        public void Run(string messageId)
        {
            SagaContext sagaContext = new SagaContext(Steps);
            sagaContext.SagaId = messageId;

            sagaContextStore.ContextStore.Add(sagaContext.SagaId,sagaContext);

            sagaContext.Execute();
        }

        protected Action<IMessageType> ContinueSaga()
        {
            return (car) =>
            {
                switch (car.Status)
                {
                    case SagaStepStatusConsts.SUCCESS:
                        {
                            sagaContextStore.ContinueSaga(car.IdMessage,false);
                            break;
                        }
                    case SagaStepStatusConsts.FAIL:
                        {
                            sagaContextStore.CancelSaga(car.IdMessage);
                            break;
                        }
                    case SagaStepStatusConsts.CANCELED:
                        {
                            sagaContextStore.ContinueSaga(car.IdMessage,true);
                            break;
                        }
                    default: throw new ArgumentException("Неизвестный тип результата саги");
                }
            };
        }
    }
}
