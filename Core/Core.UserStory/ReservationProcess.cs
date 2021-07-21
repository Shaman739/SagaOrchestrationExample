using Core.Data;
using Core.Data.Contract;
using System;

namespace Core
{
    public class ReservationProcess<T> : IReservationProcess<T> where T : ReservationItem
    {
        private IRepository<T> _repository;

        public ReservationProcess(IRepository<T> repository)
        {
            _repository = repository;
        }

        public bool Reserve(T reservationItem)
        {
            return _repository.GetAvailableItem(reservationItem.Id);
        }
    }
}
