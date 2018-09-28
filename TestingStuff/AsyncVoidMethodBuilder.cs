using System.Threading;

namespace System.Runtime.CompilerServices
{
    public class AsyncVoidMethodBuilder
    {
        private AsyncVoidMethodBuilder()
        {
            Console.WriteLine("In AsyncVoidMethodBuilder ctor");
        }

        public static AsyncVoidMethodBuilder Create() => new AsyncVoidMethodBuilder();

        public void SetResult() => Console.WriteLine("In SetResult");

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
            //AwaitOnCompleted(ref awaiter, ref stateMachine);
            var moveNextRunner = new MoveNextRunner(stateMachine);
            awaiter.UnsafeOnCompleted(moveNextRunner.MoveNextUnsafe);
        }

        public void SetException(Exception exception) { }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            Console.WriteLine("In SetStateMachine");
        }
    }

    public class MoveNextRunner
    {
        private readonly ExecutionContext _context;

        private readonly IAsyncStateMachine _stateMachine;

        public MoveNextRunner(IAsyncStateMachine stateMachine, ExecutionContext context = null)
        {
            Console.WriteLine("In MoveNextRunner ctor");
            _context = context;
            _stateMachine = stateMachine;
        }

        public void MoveNextSafe()
        {
            Console.WriteLine("In MoveNextSafe");
            ExecutionContext.Run(_context, state => _stateMachine.MoveNext(), null);
        }

        public void MoveNextUnsafe()
        {
            Console.WriteLine("In MoveNextUnsafe");
            _stateMachine.MoveNext();
        }
    }
}
