using ConsoleApp1.EventBus;
using System;

namespace Saga.Step
{
    public class SagaStep<T> :  ISagaStep
        where T: SagaStepParam
    {
        private IEventBus _eventBus;
        private string _nameExchange;
        private string _nameQueue;
        private Action<IMessageType> _callback;
        public SagaStep(T param, IEventBus eventBus, string nameExchange, string nameQueue, Action<IMessageType> callback)
        {
            Param = param;
            _eventBus = eventBus;
            _nameExchange = nameExchange;
            _nameQueue = nameQueue;
            _callback = callback;
        }
        public T Param { get; private set; }

        public void Cancel()
        {
            _eventBus.Publish<T>(_nameExchange, _nameQueue, "cancel", Param);
            _eventBus.Subscribe<T>(_nameQueue, _nameExchange, "result", _callback);
        }

        public void DoWork()
        {
            _eventBus.Publish<T>(_nameExchange, _nameQueue, "query", Param);
            _eventBus.Subscribe<T>(_nameQueue, _nameExchange, "result", _callback);
        }
    }
}
