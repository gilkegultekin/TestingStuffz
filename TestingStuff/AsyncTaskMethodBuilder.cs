using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
    public struct AsyncTaskMethodBuilder<T>
    {
        private readonly TaskCompletionSource<T> _source;

        private AsyncTaskMethodBuilder(TaskCompletionSource<T> source)
        {
            _source = source;
            Console.WriteLine("In AsyncTaskMethodBuilder ctor");
        }

        public static AsyncTaskMethodBuilder<T> Create() => new AsyncTaskMethodBuilder<T>(new TaskCompletionSource<T>());

        public void SetResult(T result)
        {
            Console.WriteLine("In SetResult");
            _source.SetResult(result);
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            Console.WriteLine("In Start");
            stateMachine.MoveNext();
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            Console.WriteLine("In AwaitOnCompleted");
            var executionContext = ExecutionContext.Capture();
            var moveNextRunner = new MoveNextRunner(stateMachine, executionContext);
            awaiter.OnCompleted(moveNextRunner.MoveNextSafe);
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter,
            ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            Console.WriteLine("In AwaitUnsafeOnCompleted");
            var moveNextRunner = new MoveNextRunner(stateMachine);
            awaiter.UnsafeOnCompleted(moveNextRunner.MoveNextUnsafe);
        }

        public void SetException(Exception exception)
        {
            Console.WriteLine("In SetException");
            _source.SetException(exception);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            Console.WriteLine("In SetStateMachine");
        }

        public Task<T> Task => _source.Task;
    }
}
