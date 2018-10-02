using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestingStuff.Coordination;

namespace TestingStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            ChunkedTest().Wait();
            Console.ReadKey();
        }

        static async Task WithAsync()
        {
            Print("WithAsync");
            var task = Task.Run(() =>
            {
                Thread.Sleep(100);
                Print("In Task.Run");
            });
            await Task.Yield();
            await task;
            await Task.Yield();
            Print("After task await");
        }

        static async Task ChunkedTest()
        {
            var httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync("http://localhost:61727/chunked");
            using (var streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    var buffer = new char[10];
                    var readLength = await streamReader.ReadAsync(buffer, 0, 10);
                    var stuff = new string(buffer, 0, readLength);
                    Console.Write(stuff);
                }
            }
        }

        static async Task WithAsync(TaskCreationOptions options)
        {
            Print($"With Async. Options: {options}");
            var tcs = new TaskCompletionSource<object>(options);
            var setTask = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Print("Setting task's result");
                tcs.SetResult(null);
                Print("Set task's result");
            });

            await tcs.Task;
            Print("After task await");
            await setTask;
        }

        static void Print(string str)
        {
            Console.WriteLine($"{str}: {Thread.CurrentThread.ManagedThreadId}");
        }

        static void InterlockedTest()
        {
            int x = 0;
            var list = new List<int> { 5, 7 };

            var autoResetEvent1 = new AutoResetEvent(false);
            var autoResetEvent2 = new AutoResetEvent(false);

            for (int i = 0; i < 2; i++)
            {
                var temp = i;
                Task.Run(() =>
                {
                    if (temp == 0)
                    {
                        if (Interlocked.Increment(ref x) >= list.Count)
                        {
                            Interlocked.Exchange(ref x, 0);
                        }
                        autoResetEvent2.Set();
                        autoResetEvent1.WaitOne();

                        Console.WriteLine($"Thread: {temp} --- {list[x]}");
                        autoResetEvent2.Set();
                    }
                    else if (temp == 1)
                    {
                        autoResetEvent2.WaitOne();
                        if (Interlocked.Increment(ref x) >= list.Count)
                        {
                            autoResetEvent1.Set();
                            autoResetEvent2.WaitOne();
                            Interlocked.Exchange(ref x, 0);
                        }

                        Console.WriteLine($"Thread: {temp} --- {list[x]}");
                    }
                });
            }
        }



        static void TryAsyncLock()
        {
            const int threadCount = 10;
            var asyncLock = new AsyncLock();

            for (int i = 0; i < threadCount; i++)
            {
                var temp = i + 1;
                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        Console.WriteLine($"Thread {temp} is on!");
                        using (await asyncLock.WaitAsync())
                        {
                            Console.WriteLine($"Thread {temp} acquired the lock!");
                            await Task.Delay(5000);
                        }

                        Console.WriteLine($"Thread {temp} has released the lock!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Thread {temp} has encountered an error: {e.Message}");
                    }
                });
            }
        }

        static void TryAsyncSemaphore()
        {
            const int resourceCount = 5;
            var asyncSemaphore = new AsyncSemaphore(resourceCount);
            const int threadCount = 15;

            for (int i = 0; i < threadCount; i++)
            {
                var temp = i + 1;
                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        await Task.Delay(1000);
                        Console.WriteLine($"Thread {temp} about to wait on the async semaphore!");
                        await asyncSemaphore.WaitAsync();
                        Console.WriteLine($"Thread {temp} has entered the async semaphore babeh!");
                        //simulate doing work
                        await Task.Delay(new Random(Guid.NewGuid().GetHashCode()).Next(7, 12) * 1000);
                        Console.WriteLine($"Thread {temp} about to release the async semaphore!");
                        asyncSemaphore.Release();
                        Console.WriteLine($"Thread {temp} has released the async semaphore!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Thread {temp} has encountered an exception: {e.Message}");
                    }
                });
            }
        }

        static void TryAsyncBarrier()
        {
            const int participantCount = 5;
            var asyncBarrier = new AsyncBarrier(participantCount, () => Console.WriteLine("AsyncBarrier is being reset!"));

            for (int i = 0; i < participantCount + 1; i++)
            {
                var temp = i + 1;
                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        //await Task.Delay(temp * 1000);
                        Console.WriteLine($"Participant {temp} signalling and waiting!");
                        await asyncBarrier.SignalAndWait();
                        Console.WriteLine($"Participant {temp} is now awake!");
                        await Task.Delay(1000);
                        Console.WriteLine($"Participant {temp} signalling and waiting for the second time!");
                        await asyncBarrier.SignalAndWait();
                        Console.WriteLine($"Participant {temp} has awoken yet again!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Participant {temp} has encountered the following exception: {e.Message}");
                    }
                });
            }
        }

        static void TryAsyncCountdownEvent()
        {
            var asyncEvent = new AsyncCountdownEvent(15);

            for (int i = 0; i < 30; i++)
            {
                var temp = i + 1;
                Task.Factory.StartNew(async () =>
                {
                    //await Task.Delay(temp * 1000);
                    Console.WriteLine($"Waiter {temp} starting to wait on the async event");
                    await asyncEvent.WaitAsync();
                    Console.WriteLine($"Waiter {temp}'s wait ended");
                });

                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        //await Task.Delay(temp * 4000);
                        Console.WriteLine($"Setter {temp} signaling the async event");
                        asyncEvent.Signal();
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine($"Setter {temp} has encountered the following exception: {e.Message}");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });
            }
        }

        static void TryAsyncManualResetEvent()
        {
            var asyncEvent = new AsyncManualResetEvent();
            Task.Factory.StartNew(async () =>
            {
                await asyncEvent.WaitAsync();
                asyncEvent.Reset();
            });

            Task.Factory.StartNew(() =>
            {
                asyncEvent.Set();
                asyncEvent.Reset();
            });
        }

        static void TryAsyncAutoResetEvent()
        {
            var asyncEvent = new AsyncAutoResetEvent();

            for (int i = 0; i < 10; i++)
            {
                var temp = i + 1;

                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(temp * 1000);
                    Console.WriteLine($"Waiter {temp} waiting on the async event");
                    await asyncEvent.WaitAsync();
                    Console.WriteLine($"Waiter {temp}'s wait ended");
                });

                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(temp * 5000);
                    Console.WriteLine($"Setter {temp} signaling the async event");
                    asyncEvent.Set();
                });
            }




        }

        static void DoSomething()
        {
            Console.WriteLine($"{Do3().Result}");
        }

        static async void Do1()
        {
            var asyncLocal = new AsyncLocal<int> { Value = 42 };
            Console.WriteLine($"Asynclocal value before first await: {asyncLocal.Value}");

            var task = Task.Delay(42);
            await task.ContinueWith(ant => Console.WriteLine($"Asynclocal value in continuewith {asyncLocal.Value}"));

            Console.WriteLine($"Asynclocal value after first await: {asyncLocal.Value}");

            var task2 = Task.Delay(42);
            task2.GetAwaiter().UnsafeOnCompleted(() => Console.WriteLine($"Asynclocal value in unsafeoncompleted {asyncLocal.Value}"));
            await task2;

            Console.WriteLine($"Asynclocal value after second await: {asyncLocal.Value}");
        }

        static async void Do2()
        {
            var task = Task.Delay(42);
            await task;

            var task2 = Task.Delay(42);
            await task2;
        }

        static async Task<int> Do3()
        {
            var task = Task.Delay(42);
            await task;

            var task2 = Task.Delay(42);
            await task2;

            return 3;
        }

        static async Task TryAggregatedExceptionAwaitable()
        {
            var t1 = Task.Run(() => throw new Exception("Exception message from task 1"));
            var t2 = Task.Run(() => throw new Exception("Exception message from task 2"));

            var t3 = Task.Run(() => throw new Exception("Exception message from task 3"));
            var t4 = Task.Run(() => throw new Exception("Exception message from task 4"));

            try
            {
                await Task.WhenAll(t1, t2);
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"Caught {e.InnerExceptions.Count} exceptions: {string.Join(", ", e.InnerExceptions.Select(ex => ex.Message))}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Type of exception: {e.GetType().Name}");
                Console.WriteLine($"Exception message: {e.Message}");
                Console.WriteLine($"Innerexception message: {e.InnerException?.Message}");
            }

            try
            {
                await Task.WhenAll(t3, t4).WithAggregatedExceptions();
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"Caught {e.InnerExceptions.Count} exceptions: {string.Join(", ", e.InnerExceptions.Select(ex => ex.Message))}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Type of exception: {e.GetType().Name}");
                Console.WriteLine($"Exception message: {e.Message}");
                Console.WriteLine($"Innerexception message: {e.InnerException?.Message}");
            }
        }


    }
}
