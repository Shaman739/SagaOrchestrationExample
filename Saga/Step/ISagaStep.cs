using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga.Step
{
    public interface ISagaStep
    {
        void DoWork();
        void Cancel();
    }
}
