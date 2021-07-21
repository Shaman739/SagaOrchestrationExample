using Core.Data;

namespace Core
{
    public interface IReservationProcess<T> where T : ReservationItem
    {
        bool Reserve(T reservationItem);
    }
}