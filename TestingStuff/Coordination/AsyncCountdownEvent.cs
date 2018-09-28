using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestingStuff.Coordination
{
    class AsyncCountdownEvent
    {
        private readonly AsyncManualResetEvent _manualResetEvent = new AsyncManualResetEvent();
        private long _initialCount;

        public AsyncCountdownEvent(int initialCount)
        {
            _initialCount = initialCount;
        }

        public Task WaitAsync()
        {
            return _manualResetEvent.WaitAsync();
        }

        public void Signal()
        {
            if (Interlocked.Read(ref _initialCount) <= 0)
            {
                throw new InvalidOperationException("The event has already been signaled bru!");
            }

            if (Interlocked.Decrement(ref _initialCount) == 0)
            {
                _manualResetEvent.Set();
            }
        }
    }
}
