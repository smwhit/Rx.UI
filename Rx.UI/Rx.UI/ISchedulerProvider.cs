using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace Rx.UI
{
    public interface ISchedulerProvider
    {
        IScheduler Dispatcher { get; }
        IScheduler TaskPool { get; }
    }
}
