using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestingStuff
{
    public class MyTask<TResult>
    {
        public bool IsCompleted { get; }

        public MyTaskAwaiter<TResult> GetAwaiter() => new MyTaskAwaiter<TResult>(this);

        public TResult Result { get; }
    }
}
