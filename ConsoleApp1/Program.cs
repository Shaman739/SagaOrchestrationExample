using ConsoleApp1.EventBus;
using Saga;
using System;

namespace ConsoleApp1
{


    class Program
    {
        static void Main(string[] args)
        {

            IEventBus eventBus = new EventBus.EventBus();

            Action<string, DateTime,string> finishCallback = delegate (string sagaId, DateTime finishTime, string status)
            {
                Console.WriteLine($"Сага {sagaId} завершилась {status} {finishTime}");
            };
            SagaContextStore sagaContext = new SagaContextStore(finishCallback);
             
            while (true)
            {
                Random random = new Random();

                ReservationTour reservationTour = new ReservationTour()
                {
                    CarId = random.Next(1, 5),
                    HotelId = random.Next(1, 4),
                    IdMessage = Guid.NewGuid().ToString()
                };

                Saga<ReservationTour> saga = new ReservationTourSaga(sagaContext, eventBus);

                saga.Create(reservationTour);
                saga.Run(reservationTour.IdMessage);
 
                Console.WriteLine("Next?");
                Console.ReadLine();
            }

            Console.ReadLine();
        }

    }
}