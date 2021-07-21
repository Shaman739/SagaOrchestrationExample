using Core;
using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCarReservation
{
    public class HotelReservationProcess : IReservationProcess<ReservationItem>
    {
        public Dictionary<int, string> availbleData = new Dictionary<int, string>();
        public HotelReservationProcess()
        {
            availbleData.Add(1, "HotelId_1");
            availbleData.Add(2, "HotelId_2");
            availbleData.Add(3, "HotelId_3");
        }
        public bool Reserve(ReservationItem reservationItem)
        {
            return availbleData.TryGetValue(reservationItem.Id,out _);
        }
    }
}
