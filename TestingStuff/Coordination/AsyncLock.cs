using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestingStuff.Coordination
{
    class AsyncLock
    {
        private readonly AsyncSemaphore _asyncSemaphore = new AsyncSemaphore(1);
        private readonly Task<Releaser> _completedTask;

        public AsyncLock()
        {
            _completedTask = Task.FromResult(new Releaser(this));
        }

        public Task<Releaser> WaitAsync()
        {
            var waitTask = _asyncSemaphore.WaitAsync();
            return waitTask.IsCompleted
                ? _completedTask
                : waitTask.ContinueWith((_, state) => new Releaser((AsyncLock) state), this);
                //waitTask.ContinueWith(ant => new Releaser(this));
            /*waitTask.ContinueWith((_, state) => new Releaser((AsyncLock)state), this, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default)*/;
        }

        public struct Releaser : IDisposable
        {
            private readonly AsyncLock _asyncLock;

            public Releaser(AsyncLock asyncLock)
            {
                _asyncLock = asyncLock;
            }

            public void Dispose()
            {
                _asyncLock?._asyncSemaphore.Release();
            }
        }
    }


}
