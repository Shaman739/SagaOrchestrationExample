using System;

namespace Core.Data.Contract.EventBus
{
    public interface IEventBus
    {
        void Publish<TMessage>(string nameExchange, string nameQueue, string routingKey, TMessage message) where TMessage: IMessageType;
        void Subscribe<TMessage>(string queue, string exchange, string routingKey, Action<TMessage> callbackSubcribe) where TMessage : IMessageType;
    }
}