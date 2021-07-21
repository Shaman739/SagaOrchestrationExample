using Core;
using Core.Data;
using Core.Data.Contract.EventBus;
using EventBus;
using Saga;
using Saga.Step;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCarReservation
{
    public class CarReservationQueueHandler
    {
        public CarReservationQueueHandler(IReservationProcess<ReservationItem> reservationProcess,IEventBus eventBus)
        {
            RoutingEqueuTopic routingEqueuTopic = new RoutingEqueuTopic("car");

            eventBus.Subscribe<CarParam>(routingEqueuTopic.QueryEnqueu, routingEqueuTopic.NameExchange, routingEqueuTopic.QueryRoutingKey, (message) =>
            {
                if (reservationProcess.Reserve(new ReservationItem() { Id = message.CarId}))
                    message.Status = SagaStepStatusConsts.SUCCESS;
                else
                    message.Status = SagaStepStatusConsts.FAIL;
                eventBus.Publish<CarParam>(routingEqueuTopic.NameExchange, routingEqueuTopic.ResultEnqueu, routingEqueuTopic.ResultRoutingKey, message);

            });

            eventBus.Subscribe<CarParam>(routingEqueuTopic.QueryEnqueu, routingEqueuTopic.NameExchange, routingEqueuTopic.CancelRoutingKey, (message) =>
            {
                message.Status = SagaStepStatusConsts.CANCELED;
                eventBus.Publish<CarParam>(routingEqueuTopic.NameExchange, routingEqueuTopic.ResultEnqueu, routingEqueuTopic.ResultRoutingKey, message);

            });
        }
    }
}
