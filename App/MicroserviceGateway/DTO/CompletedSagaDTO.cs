using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceGateway.DTO
{
    public class CompletedSagaDTO
    {
        public string SagaId { get; set; }

        public int CountSuccessSteps { get; set; }
        public int CountNotExecutedSteps { get; set; }
        public bool isSuccess  { get; set; }
    }
}
