using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TestingStuff
{
    public class AggregatedExceptionAwaiter : INotifyCompletion
    {
        private readonly Task _task;

        public AggregatedExceptionAwaiter(Task task)
        {
            _task = task;
        }

        public bool IsCompleted => _task.IsCompleted;

        public void GetResult()
        {
            _task.Wait();
        }

        public void OnCompleted(Action continuation)
        {
            _task.GetAwaiter().OnCompleted(continuation);
        }
    }
}
