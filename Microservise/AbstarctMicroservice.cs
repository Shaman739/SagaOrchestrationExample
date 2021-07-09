using ConsoleApp1.EventBus;
using System.Collections.Generic;

namespace Microservise
{
    public class RoutingEqueuTopic
    {
        public readonly string NameExchange;
        public string QueryEnqueu => $"{NameExchange}";
        public string ResultEnqueu => $"{NameExchange}";

        public string QueryRoutingKey => "query";
        public string ResultRoutingKey => "result";
        public string CancelRoutingKey => "cancel";
        public RoutingEqueuTopic(string nameExchange)
        {
            NameExchange = nameExchange;
        }
    }

   
    public class AbstarctMicroservice<TMessage> where TMessage : IMessageType
    {
        protected RoutingEqueuTopic routingEqueuTopic;
        protected IEventBus _eventBus;

        public AbstarctMicroservice(string nameQueue, IEventBus eventBus)
        {
            routingEqueuTopic = new RoutingEqueuTopic(nameQueue);
            _eventBus = eventBus;
        }

        public void SendResult( string queue,string routingKey, TMessage value)
        {
            _eventBus.Publish<TMessage>(routingEqueuTopic.NameExchange, queue, routingKey, value);
        }

        public Dictionary<int, string> availbleData = new Dictionary<int, string>();

    }
}
