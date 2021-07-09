using ConsoleApp1.EventBus;
using Core;
using Core.EventBus;
using Newtonsoft.Json;
using Saga;
using Saga.Step;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservise
{
    public class CarMicroservice : AbstarctMicroservice<CarParam>
    {
        public CarMicroservice(IEventBus eventBus) : base("car", eventBus)
        {
            routingEqueuTopic = new RoutingEqueuTopic("car");
            availbleData.Add(1, "CarId_1");
            availbleData.Add(2, "CarId_2");
            availbleData.Add(3, "CarId_3");
           
            eventBus.Subscribe<CarParam>(routingEqueuTopic.QueryEnqueu, routingEqueuTopic.NameExchange, routingEqueuTopic.QueryRoutingKey, (message) =>
            {
                Console.WriteLine("Recived {0} {1} ", message.IdMessage, JsonConvert.SerializeObject(message));
              
                if (availbleData.TryGetValue(message.CarId, out _))
                    message.Status = SagaStepStatusConsts.SUCCESS;
                else
                    message.Status = SagaStepStatusConsts.FAIL;
                _eventBus.Publish<CarParam>(routingEqueuTopic.NameExchange, routingEqueuTopic.ResultEnqueu, routingEqueuTopic.ResultRoutingKey, message);

            });

            eventBus.Subscribe<CarParam>(routingEqueuTopic.QueryEnqueu, routingEqueuTopic.NameExchange, routingEqueuTopic.CancelRoutingKey, (message) =>
            {
                Console.WriteLine("Recived Cancel {0} {1} ", message.IdMessage, JsonConvert.SerializeObject(message));
               
                message.Status = SagaStepStatusConsts.CANCELED;
                _eventBus.Publish<CarParam>(routingEqueuTopic.NameExchange, routingEqueuTopic.ResultEnqueu, routingEqueuTopic.ResultRoutingKey, message);

            });
        }
    }
}
