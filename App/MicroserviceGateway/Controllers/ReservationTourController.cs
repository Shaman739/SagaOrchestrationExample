using Core.Data.Contract.EventBus;
using MicroserviceGateway.Reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationTourController : ControllerBase
    {
        private readonly ILogger<ReservationTourController> _logger;
        private readonly IEventBus _eventBus;
        private readonly SagaContextStore _sagaContextStore;

        public ReservationTourController(ILogger<ReservationTourController> logger, IEventBus eventBus, SagaContextStore sagaContextStore)
        {
            _logger = logger;
            _eventBus = eventBus;
            _sagaContextStore = sagaContextStore;
        }

        [HttpGet]
        public IActionResult Reserve([FromQuery] int car_id, [FromQuery] int hotel_id)
        {
            ReservationTour reservationTour = new ReservationTour()
            {
                CarId = car_id,
                HotelId = hotel_id,
                IdMessage = Guid.NewGuid().ToString()
            };

            Saga<ReservationTour> saga = new ReservationTourSaga(_sagaContextStore, _eventBus);

            saga.Create(reservationTour);
            saga.Run(reservationTour.IdMessage);

            return Ok();
        }
    }
}
