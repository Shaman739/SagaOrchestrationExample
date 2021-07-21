using Saga.Step;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga
{
    public class SagaContext
    {
        public SagaContext(List<ISagaStep> steps)
        {
            NextSteps = new Queue<ISagaStep>(steps);
            CompletedSteps = new Stack<ISagaStep>();
        }
        public Queue<ISagaStep> NextSteps { get; set; }
        public Stack<ISagaStep> CompletedSteps { get; set; }

        public string SagaId { get; set; }

        internal void SuccessCurrentStep()
        {
            ISagaStep step = NextSteps.Dequeue();
            CompletedSteps.Push(step);
        }

        public bool IsCompleted { get { return NextSteps.Count == 0; } }

        public bool IsCanceled { get { return CompletedSteps.Count == 0; } }

        public bool IsError { get; set; }

        public void Execute()
        {
            if (!IsCompleted && !IsError)
            {
                ISagaStep step = NextSteps.Peek();
                step.DoWork();
            }
            else if (IsError && !IsCanceled)
                CompletedSteps.Pop().Cancel();

        }

    }
}
