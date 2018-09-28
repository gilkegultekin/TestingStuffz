using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingStuff.Coordination
{
    class AsyncAutoResetEvent
    {
        private readonly Task<bool> _completedTask = Task.FromResult(true);
        private readonly Queue<TaskCompletionSource<bool>> _waitingList = new Queue<TaskCompletionSource<bool>>();
        private bool _signaled;

        public Task WaitAsync()
        {
            lock (_waitingList)
            {
                if (_signaled)
                {
                    _signaled = false;
                    return _completedTask;
                }

                var tcs = new TaskCompletionSource<bool>();
                _waitingList.Enqueue(tcs);
                return tcs.Task;
            }
        }

        public void Set()
        {
            TaskCompletionSource<bool> nextTask = null;
            lock (_waitingList)
            {
                if (_waitingList.Any())
                {
                    nextTask = _waitingList.Dequeue();
                }
                else
                {
                    _signaled = true;
                }
            }

            nextTask?.SetResult(true);
        }
    }
}
