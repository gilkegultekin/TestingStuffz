using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestingStuff
{
    public class AsyncLocalTest
    {
        public void Test()
        {
            Console.WriteLine("Test");
            Console.WriteLine($"Start of method - {Thread.CurrentThread.ManagedThreadId} - {AmbientStuff.Current?.Id} - {AmbientStuff.Current?.Value}");
            DoThings(3, 1);
            DoThings(5, 2);
            
            Console.WriteLine($"End of method - {Thread.CurrentThread.ManagedThreadId} - {AmbientStuff.Current?.Id} - {AmbientStuff.Current?.Value}");
        }

        private void DoThings(int ambientValue, int taskValue)
        {
            Task.Run(async () =>
            {
                using (AmbientStuff.Change(new AmbientStuff(ambientValue)))
                {
                    await DoStuff(taskValue);
                }
            });
        }

        private async Task DoStuff(int id)
        {
            await Task.Run(async () =>
            {
                Console.WriteLine($"Task {id} - before await - {Thread.CurrentThread.ManagedThreadId} - {AmbientStuff.Current.Id} - {AmbientStuff.Current.Value}");
                await Task.Delay(1000);
                Console.WriteLine($"Task {id} - after await - {Thread.CurrentThread.ManagedThreadId} - {AmbientStuff.Current.Id} - {AmbientStuff.Current.Value}");
            });
        }
    }

    public class AmbientStuff
    {
        public static AmbientStuff Current
        {
            get
            {
                Console.WriteLine("Getting AmbientStuff current");
                return _current.Value;
            }
            set
            {
                Console.WriteLine("Setting AmbientStuff current");
                _current.Value = value;
            }
        }

        private static readonly AsyncLocal<AmbientStuff> _current;

        static AmbientStuff()
        {
            Console.WriteLine("In Ambient stuff static ctor, initializing _current");
            _current = new AsyncLocal<AmbientStuff>();
        }

        public Guid Id { get; }

        public int Value { get; }

        public AmbientStuff(int value)
        {
            Id = Guid.NewGuid();
            Value = value;
        }

        public static IDisposable Change(AmbientStuff stuff)
        {
            var oldValue = Current;
            Current = stuff;
            return new DisposeAction(() =>
            {
                Current = oldValue;
            });
        }
    }

    public class DisposeAction : IDisposable
    {
        private readonly Action _action;

        public DisposeAction(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}
