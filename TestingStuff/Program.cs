using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Algorithms;
using TestingStuff.Coordination;

namespace TestingStuff
{
    class TestState
    {
        public int Counter { get; set; }
        public AutoResetEvent AutoResetEvent { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //WithAsync().Wait();
            //WithAsync(TaskCreationOptions.None).Wait();
            //WithAsync(TaskCreationOptions.RunContinuationsAsynchronously).Wait();
            //TaskFactoryMultipleTest();
            //TryAggregatedExceptionAwaitable().Wait();
            //Console.WriteLine($"Solution: {PeakFinder.Run1DSample()}");
            //LengthOfLongestSubstring();
            IsomorphicStrings();

            Console.ReadKey();
        }

        static void IsomorphicStrings()
        {
            var s = "ab";
            var t = "aa";
            var solution = new IsomorphicStringsSolution();
            Console.WriteLine(solution.Solve(s,t));
        }

        static void LengthOfLongestSubstring()
        {
            var s = "pwzwpkew";
            Console.WriteLine(new LengthOfLongestSubstringSolution().Solve(s));
        }

        static void AddTwoNumbers()
        {
            var l1 = new ListNode(0);
            var l2 = new ListNode(0);
            l1.next = l2;
            var l3 = new ListNode(9);
            l2.next = l3;

            var r1 = new ListNode(0);
            var r2 = new ListNode(0);
            r1.next = r2;
            var r3 = new ListNode(5);
            r2.next = r3;
            var r4 = new ListNode(9);
            r3.next = r4;

            var solution = new AddTwoNumbersSolution().Solve(l1, r1);
            foreach (var value in solution.Values().Reverse())
            {
                Console.Write(value);
            }
        }

        static void TaskFactoryMultipleTest()
        {
            const int a = 50;
            var list = new Task[a];
            for (int i = 0; i < a; i++)
            {
                var temp = i;
                list[i] = Task.Factory.StartNew(() => TaskFactoryTestInternal(temp).Wait());
            }

            Task.WaitAll(list);
            Print("COMPLETED!!!!");
            //Console.WriteLine("COMPLETED!!!!");
        }

        static async Task TaskFactoryTestInternal(int counter)
        {
            Print($"{counter} STARTED");
            var are = new AutoResetEvent(false);
            //var are2 = new AutoResetEvent(false);
            var testState = new TestState
            {
                Counter = counter,
                AutoResetEvent = are
            };

            //var t1 = Task.Factory.StartNew(T1, testState);
            //var t2 = Task.Factory.StartNew(T2, testState);

            var t1 = Task.Run(() =>
            {
                T1(counter, are);
                //are2.Set();
            });
            Print($"{counter}: T1 task created", ConsoleColor.DarkYellow);
            //t1.Wait();
            var t2 = Task.Run(() =>
            {
                //are2.WaitOne();
                T2(counter, are);
            });
            Print($"{counter}: T2 task created", ConsoleColor.Yellow);

            //var t1 = Task.Factory.StartNew(() => T1(counter, are), TaskCreationOptions.LongRunning);
            //var t2 = Task.Factory.StartNew(() => T2(counter, are), TaskCreationOptions.LongRunning);

            //var t2 = Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine($"{counter}: T2 started");
            //    are.WaitOne();
            //    Thread.Sleep(1000);
            //    Console.WriteLine($"{counter}: T2 complete");
            //});

            //Task.WaitAll(t1, t2);
            //Task.WaitAll(t2, t1);
            await Task.WhenAll(t1, t2);
            //Console.WriteLine($"{counter} FINISHED");
            Print($"{counter} FINISHED");
        }

        static void T1(int counter, AutoResetEvent are)
        {
            //Console.WriteLine($"{counter}: T1 started");
            Print($"{counter}: T1 started", ConsoleColor.Red);
            Print($"{counter}: T1 - {TaskScheduler.Current == TaskScheduler.Default}");
            Thread.Sleep(1000);
            are.Set();
            Print($"{counter}: T1 complete", ConsoleColor.DarkRed);
            //Console.WriteLine($"{counter}: T1 complete");
        }

        static void T2(int counter, AutoResetEvent are)
        {
            //Console.WriteLine($"{counter}: T2 started");
            Print($"{counter}: T2 started", ConsoleColor.Blue);
            Print($"{counter}: T2 - {TaskScheduler.Current == TaskScheduler.Default}");
            are.WaitOne();
            Thread.Sleep(1000);
            //Console.WriteLine($"{counter}: T2 complete");
            Print($"{counter}: T2 complete", ConsoleColor.DarkBlue);
        }

        static void T1(object state)
        {
            var s = (TestState) state;
            Console.WriteLine($"{s.Counter}: T1 started");
            Thread.Sleep(1000);
            s.AutoResetEvent.Set();
            Console.WriteLine($"{s.Counter}: T1 complete");
        }

        static void T2(object state)
        {
            var s = (TestState)state;
            Console.WriteLine($"{s.Counter}: T2 started");
            s.AutoResetEvent.WaitOne();
            Thread.Sleep(1000);
            Console.WriteLine($"{s.Counter}: T2 complete");
        }

        static void UriBuilderTest()
        {
            UriBuilderTestWithBaseAddress("https://blabla.sestek.com/api/1.1/messages", "send", new Dictionary<string, string> { { "foo", "bar" }, { "hede", "hödö" } });
            UriBuilderTestWithoutBaseAddress("https://blabla.sestek.com/api/1.1/messages/send", new Dictionary<string, string> { { "foo", "bar" }, { "hede", "hödö" } });
            UriBuilderTestWithoutBaseAddress2("send", new Dictionary<string, string> { { "foo", "bar" }, { "hede", "hödö" } });
        }

        static void UriBuilderTestWithoutBaseAddress2(string relativePath, Dictionary<string, string> queryParams)
        {
            Console.WriteLine();

            var uriBuilder = new UriBuilder
            {
                Path = relativePath
            };

            if (queryParams != null && queryParams.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                foreach (var parameter in queryParams)
                {
                    query[HttpUtility.UrlEncode(parameter.Key, Encoding.UTF8)] = HttpUtility.UrlEncode(parameter.Value, Encoding.UTF8);
                }
                Console.WriteLine($"Raw Query:{query}");
                uriBuilder.Query = query.ToString();
            }
            Console.WriteLine("WITHOUT BASE ADDRESS 2");
            Console.WriteLine($"Scheme: {uriBuilder.Scheme}");
            Console.WriteLine($"Host: {uriBuilder.Host}");
            Console.WriteLine($"Port: {uriBuilder.Port}");
            Console.WriteLine($"Fragment: {uriBuilder.Fragment}");
            Console.WriteLine($"Path: {uriBuilder.Path}");
            Console.WriteLine($"Query: {uriBuilder.Query}");
            Console.WriteLine(uriBuilder.Uri);
        }

        static void UriBuilderTestWithoutBaseAddress(string endpointUri, Dictionary<string, string> queryParams)
        {
            Console.WriteLine();

            var uriBuilder = new UriBuilder(endpointUri);

            if (queryParams != null && queryParams.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                foreach (var parameter in queryParams)
                {
                    query[HttpUtility.UrlEncode(parameter.Key, Encoding.UTF8)] = HttpUtility.UrlEncode(parameter.Value, Encoding.UTF8);
                }
                Console.WriteLine($"Raw Query:{query}");
                uriBuilder.Query = query.ToString();
            }
            Console.WriteLine("WITHOUT BASE ADDRESS");
            Console.WriteLine($"Scheme: {uriBuilder.Scheme}");
            Console.WriteLine($"Host: {uriBuilder.Host}");
            Console.WriteLine($"Port: {uriBuilder.Port}");
            Console.WriteLine($"Fragment: {uriBuilder.Fragment}");
            Console.WriteLine($"Path: {uriBuilder.Path}");
            Console.WriteLine($"Query: {uriBuilder.Query}");
            Console.WriteLine(uriBuilder.Uri);
        }

        static void UriBuilderTestWithBaseAddress(string baseAddress, string action, Dictionary<string, string> queryParams)
        {
            Console.WriteLine();

            var uriBuilder = new UriBuilder(baseAddress.EndsWith("/") ? baseAddress : $"{baseAddress}/");
            uriBuilder.Path += action;

            if (queryParams != null && queryParams.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                foreach (var parameter in queryParams)
                {
                    query[parameter.Key] = parameter.Value;
                }
                Console.WriteLine($"Raw Query:{query}");
                uriBuilder.Query = query.ToString();
            }
            Console.WriteLine("WITH BASE ADDRESS");
            Console.WriteLine($"Scheme: {uriBuilder.Scheme}");
            Console.WriteLine($"Host: {uriBuilder.Host}");
            Console.WriteLine($"Port: {uriBuilder.Port}");
            Console.WriteLine($"Fragment: {uriBuilder.Fragment}");
            Console.WriteLine($"Path: {uriBuilder.Path}");
            Console.WriteLine($"Query: {uriBuilder.Query}");
            Console.WriteLine(uriBuilder.Uri);
        }

        static void MonitorPulseTest()
        {
            var syncRoot = new object();

            for (int i = 0; i < 2; i++)
            {
                var temp = i;
                Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        Thread.Sleep(new Random(Guid.NewGuid().GetHashCode()).Next(1, 3) * 1000);
                        Console.WriteLine($"NoPulse{temp} is awake!");
                        lock (syncRoot)
                        {
                            Console.WriteLine($"NoPulse{temp} has acquired the lock!");
                            Thread.Sleep(new Random(Guid.NewGuid().GetHashCode()).Next(3, 5) * 1000);
                        }
                        Console.WriteLine($"NoPulse{temp} has released the lock!");
                    }
                }, TaskCreationOptions.LongRunning);

                Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        Console.WriteLine($"PULSE{temp} is awake!");
                        lock (syncRoot)
                        {
                            Console.WriteLine($"PULSE{temp} is about to wait on the monitor!");
                            Monitor.Wait(syncRoot);
                            Console.WriteLine($"PULSE{temp} has reacquired the lock!");
                            Thread.Sleep(new Random(Guid.NewGuid().GetHashCode()).Next(3, 5) * 1000);
                        }
                        Console.WriteLine($"PULSE{temp} has released the lock!");
                        Thread.Sleep(new Random(Guid.NewGuid().GetHashCode()).Next(5, 7) * 1000);
                    }
                }, TaskCreationOptions.LongRunning);
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(20000);
                    Console.WriteLine("About to pulse...");
                    lock (syncRoot)
                    {
                        Monitor.PulseAll(syncRoot);
                        Console.WriteLine("PULSE COMPLETE...");
                    }
                }
            }, TaskCreationOptions.LongRunning).Wait();
        }

        static async Task WithAsync()
        {
            Print("WithAsync");
            //var task = Task.Run(() =>
            //{
            //    Thread.Sleep(10000);
            //    Print("In Task.Run");
            //});
            var task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(10000);
                Print("In Task.Run");
            }, TaskCreationOptions.RunContinuationsAsynchronously);
            await Task.Yield();
            Print("After first yield");
            await task;
            Print("After task await");
            await Task.Yield();
            Print("After second yield");
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

        static void Print(string str, ConsoleColor color = ConsoleColor.White)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"{str}: {Thread.CurrentThread.ManagedThreadId}");
            Console.ForegroundColor = oldColor;
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

    //class Animal
    //{
    //    protected virtual void Breathe() { }
    //}

    //class Giraffe : Animal
    //{
    //    public void DoSomething()
    //    {
    //        Giraffe g1 = new Giraffe();
    //        g1.Breathe(); //legal
    //        Animal g2 = new Zebra();
    //        g2.Breathe(); //illegal
    //    }
    //}

    //class Zebra : Animal
    //{
    //    protected override void Breathe()
    //    {
    //        base.Breathe();
    //    }
    //}
}
