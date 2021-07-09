using ConsoleApp1.EventBus;
using Core;
using Newtonsoft.Json;
using Saga;
using Saga.Step;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microservise
{
    public class HotelMicroservice : AbstarctMicroservice<HotelParam>
    {
        public HotelMicroservice(IEventBus eventBus) : base("hotel", eventBus)
        {
            routingEqueuTopic = new RoutingEqueuTopic("hotel");
            availbleData.Add(1, "HotelId_1");
            availbleData.Add(2, "HotelId_2");
            availbleData.Add(3, "HotelId_3");

            eventBus.Subscribe<HotelParam>(routingEqueuTopic.QueryEnqueu, routingEqueuTopic.NameExchange, routingEqueuTopic.QueryRoutingKey, (message) =>
            {
               // Thread.Sleep(4000);
                Console.WriteLine("Recived {0} {1} ", message.IdMessage, JsonConvert.SerializeObject(message));
                if (availbleData.TryGetValue(message.HotelId, out _))
                    message.Status = SagaStepStatusConsts.SUCCESS;
                else
                    message.Status = SagaStepStatusConsts.FAIL;
                _eventBus.Publish<HotelParam>(routingEqueuTopic.NameExchange, routingEqueuTopic.ResultEnqueu, routingEqueuTopic.ResultRoutingKey, message);

            });

            eventBus.Subscribe<HotelParam>(routingEqueuTopic.QueryEnqueu, routingEqueuTopic.NameExchange, routingEqueuTopic.CancelRoutingKey, (message) =>
            {
                Console.WriteLine("Recived Cancel {0} {1} ", message.IdMessage, JsonConvert.SerializeObject(message));
                message.Status = SagaStepStatusConsts.CANCELED;
                _eventBus.Publish<HotelParam>(routingEqueuTopic.NameExchange, routingEqueuTopic.ResultEnqueu, routingEqueuTopic.ResultRoutingKey, message);

            });
        }
    }
    
    
}
