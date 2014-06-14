using Microsoft.Reactive.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace Rx.UI.Tests
{
    public class TestSchedulerProvider : ISchedulerProvider
    {
        private readonly TestScheduler _dispatcher = new TestScheduler();

        private readonly TestScheduler _taskPool = new TestScheduler();
        public TestScheduler Dispatcher { get { return _dispatcher; } }

        public TestScheduler TaskPool { get { return _taskPool; } }

        IScheduler ISchedulerProvider.Dispatcher
        {
            get { return _dispatcher; }
        }

        IScheduler ISchedulerProvider.TaskPool
        {
            get { return _taskPool; }
        }
    }
}
