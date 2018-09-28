using System;
using System.Runtime.CompilerServices;

namespace TestingStuff
{
    public struct MyTaskAwaiter<TResult> : ICriticalNotifyCompletion
    {
        private readonly MyTask<TResult> _task;

        public MyTaskAwaiter(MyTask<TResult> task)
        {
            _task = task;
        }

        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }

        public TResult GetResult() => _task.Result;

        //public bool IsCompleted => _task.IsCompleted;
    }
}
