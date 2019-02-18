﻿using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.TutorialOne.GrainInterfaces;
using System;
using System.Threading.Tasks;

namespace Orleans.TutorialOne.Client
{
    class Program
    {
        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;

            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansTutorialOne";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            // example of calling grains from the initialized client
            //var friend = client.GetGrain<IHello>(0);
            //var response = await friend.SayHello("Good morning, HelloGrain!");
            //Console.WriteLine($"\n\n{response}\n\n");

            var testGrain = client.GetGrain<IStorageTestGrain>(0);

            //await testGrain.WriteValueToStorage("hello");

            Console.WriteLine($"Current value from storage: {await testGrain.ProvideValueFromStorage()}");
            Console.WriteLine("Please provide new value to override...");

            var newVal = Console.ReadLine();
            await testGrain.WriteValueToStorage(newVal);
            Console.WriteLine($"Current value from storage: {await testGrain.ProvideValueFromStorage()}");
        }
    }
}
