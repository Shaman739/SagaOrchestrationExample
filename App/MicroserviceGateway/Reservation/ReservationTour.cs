using Saga;

namespace MicroserviceGateway.Reservation
{
    public class ReservationTour : SagaParam
    {
        public int CarId { get; set; }
        public int HotelId { get; set; }
    }
}
