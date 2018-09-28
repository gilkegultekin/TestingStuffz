using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingStuff.Coordination
{
    class AsyncSemaphore
    {
        private readonly Queue<TaskCompletionSource<bool>> _waiterList = new Queue<TaskCompletionSource<bool>>();
        private int _currentCount;
        private readonly Task _completedTask = Task.CompletedTask;

        public AsyncSemaphore(int initialCount)
        {
            _currentCount = initialCount;
        }

        public Task WaitAsync()
        {
            lock (_waiterList)
            {
                if (_currentCount > 0)
                {
                    --_currentCount;
                    return _completedTask;
                }

                var tcs = new TaskCompletionSource<bool>();
                _waiterList.Enqueue(tcs);
                return tcs.Task;
            }
        }

        public void Release()
        {
            TaskCompletionSource<bool> nextInLine = null;
            lock (_waiterList)
            {
                if (_waiterList.Any())
                {
                    nextInLine = _waiterList.Dequeue();
                }
                else
                {
                    _currentCount++;
                }
            }

            nextInLine?.SetResult(true);
        }
    }
}
