using System;
using System.Collections.Immutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestingStuff
{
    public class AsyncLocalTest
    {
        public async Task Test()
        {
            WriteToConsole("Before using");
            
            using (AmbientStuff.Change(new AmbientStuff(0, ImmutableStack<string>.Empty.Push("Test"))))
            {
                WriteToConsole("Inside using - Before DoThings");
                var t1 = DoThings(3, 1);
                //var t2 = DoThings(5, 2);

                await Task.WhenAll(t1);
                WriteToConsole("Inside using - After DoThings");
            }

            WriteToConsole("After using");
        }

        private void WriteToConsole(string msg)
        {
            Console.WriteLine($"{GetAmbientInfo} - {msg}");
        }

        private string GetAmbientInfo => $"-----------------------------------\nStack: {AmbientStuff.Current?.GetCurrentStackAsString() ?? "Empty"}\nThreadId: {Thread.CurrentThread.ManagedThreadId}\nAmbientId: {AmbientStuff.Current?.Id ?? Guid.Empty}\nAmbientValue: {AmbientStuff.Current?.Value ?? -1}\n";

        private async Task DoThings(int ambientValue, int taskValue)
        {
            using (AmbientStuff.Change(new AmbientStuff(AmbientStuff.Current.Value, AmbientStuff.Current.Stack.Push("DoThings"))))
            {
                await Task.Run(async () =>
                {
                    WriteToConsole($"Task {taskValue} - before using");
                    using (AmbientStuff.Change(new AmbientStuff(ambientValue, AmbientStuff.Current.Stack)))
                    {
                        WriteToConsole($"Task {taskValue} - inside using, before await DoStuff2");
                        await DoStuff2(taskValue, ambientValue * 3);
                        WriteToConsole($"Task {taskValue} - inside using, after await DoStuff2");
                    }
                    WriteToConsole($"Task {taskValue} - after using");
                });
            }
        }

        private async Task DoStuff(int id)
        {
            using (AmbientStuff.Change(new AmbientStuff(AmbientStuff.Current.Value, AmbientStuff.Current.Stack.Push("DoStuff"))))
            {
                await Task.Run(async () =>
                {
                    WriteToConsole($"Task {id} - before await");
                    await Task.Delay(1000);
                    WriteToConsole($"Task {id} - after await");
                });
            }
        }

        private async Task DoStuff2(int id, int newValue)
        {
            using (AmbientStuff.Change(new AmbientStuff(AmbientStuff.Current.Value, AmbientStuff.Current.Stack.Push("DoStuff2"))))
            {
                await Task.Run(async () =>
                {
                    WriteToConsole($"Task {id} - before using");
                    using (AmbientStuff.Change(new AmbientStuff(newValue, AmbientStuff.Current.Stack)))
                    {
                        WriteToConsole($"Task {id} - inside using, before await");
                        await DoStuff(id);
                        WriteToConsole($"Task {id} - inside using, after await");
                    }
                    WriteToConsole($"Task {id} - after using");
                });
            }
        }
    }

    public class AmbientStuff
    {
        public static AmbientStuff Current
        {
            get
            {
                //Console.WriteLine("Getting AmbientStuff current");
                return _current.Value;
            }
            set
            {
                //Console.WriteLine("Setting AmbientStuff current");
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

        public ImmutableStack<string> Stack { get; }

        public string GetCurrentStackAsString()
        {
            var sb = new StringBuilder();
            foreach (var method in Stack)
            {
                sb.Append($"{method} - ");
            }

            var result = sb.ToString();
            return string.IsNullOrWhiteSpace(result) ? result : result.Substring(0, result.Length - 1);
        }

        public AmbientStuff(int value, ImmutableStack<string> stack)
        {
            Id = Guid.NewGuid();
            Value = value;
            Stack = stack;
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
