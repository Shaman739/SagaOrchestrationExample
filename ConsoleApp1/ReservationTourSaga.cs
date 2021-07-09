using ConsoleApp1.EventBus;
using Saga.Step;
using System;

namespace Saga
{
    public class ReservationTourSaga : Saga<ReservationTour>
    {
        IEventBus _eventBus;
        public ReservationTourSaga(SagaContextStore sagaContextStore, IEventBus eventBus) : base(sagaContextStore)
        {
            _eventBus = eventBus;
        }

        public override void Create(ReservationTour param)
        {
            CarParam carParam = new CarParam()
            {
                CarId = param.CarId,
                IdMessage = param.IdMessage
            };

            HotelParam hotelParam = new HotelParam()
            {
                HotelId = param.HotelId,
                IdMessage = param.IdMessage
            };

            AddStep(new SagaStep<CarParam>(
                param: carParam,
                eventBus: _eventBus,
                nameExchange: "car",
                nameQueue: "car",
                callback: ContinueSaga()
                )
                );
            AddStep(new SagaStep<HotelParam>(
                param: hotelParam,
                eventBus: _eventBus,
                nameExchange: "hotel",
                nameQueue: "hotel",
                callback: ContinueSaga()
                )
                );
        }
    }
}
