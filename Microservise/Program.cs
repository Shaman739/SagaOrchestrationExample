using ConsoleApp1.EventBus;
using Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Microservise
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("1 - car");
            Console.WriteLine("2 - hotel");
            IEventBus eventBus = new EventBus();
            int type = Convert.ToInt32(Console.ReadLine());

            switch (type)
            {
                case 1: {  new CarMicroservice(eventBus); break; };
                case 2: {  new HotelMicroservice(eventBus); break; };
            }
            Console.ReadLine();
           
        }
    }
}
