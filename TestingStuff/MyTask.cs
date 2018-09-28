using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestingStuff
{
    public class MyTask<TResult>
    {
        private bool _isCompleted;

        public bool isCompleted => _isCompleted;

        public MyTaskAwaiter<TResult> GetAwaiter() => new MyTaskAwaiter<TResult>(this);

        public TResult Result { get; }
    }
}
