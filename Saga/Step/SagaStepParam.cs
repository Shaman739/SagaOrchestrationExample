
using ConsoleApp1.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga.Step
{
    public class SagaStepParam : IMessageType
    {
        public string IdMessage { get; set; }
        public string Status { get; set; }
    }
}
