using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga
{
    public class ReservationTour : SagaParam
    {
        public int CarId { get; set; }
        public int HotelId { get; set; }
    }
}
