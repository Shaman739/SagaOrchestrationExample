using Core;
using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCarReservation
{
    public class CarReservationProcess : IReservationProcess<ReservationItem>
    {
        public Dictionary<int, string> availbleData = new Dictionary<int, string>();
        public CarReservationProcess()
        {
            availbleData.Add(1, "CarId_1");
            availbleData.Add(2, "CarId_2");
            availbleData.Add(3, "CarId_3");
        }
        public bool Reserve(ReservationItem reservationItem)
        {
            return availbleData.TryGetValue(reservationItem.Id,out _);
        }
    }
}
