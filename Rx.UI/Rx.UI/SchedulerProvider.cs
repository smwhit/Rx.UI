using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace Rx.UI
{
    public class SchedulerProvider : ISchedulerProvider
    {
        public IScheduler Dispatcher
        {
            get { return DispatcherScheduler.Current; }
        }

        public IScheduler TaskPool
        {
            get { return TaskPoolScheduler.Default; }
        }
    }
}
