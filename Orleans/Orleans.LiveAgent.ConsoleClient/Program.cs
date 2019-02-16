using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Orleans.LiveAgent.ConsoleClient
{
    class Program
    {
        private static HubConnection _connection;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to LiveAgent...");
            DoWork().Wait();
        }

        private static async Task DoWork()
        {
            await ConnectToHub();

            while (true)
            {
                var msg = Console.ReadLine();
                await _connection.InvokeAsync("SendMessageToGrain", msg);
            }
        }

        private static async Task ConnectToHub()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:61746/liveAgent")
                .Build();

            _connection.On<string>("ReceiveMessage", message =>
            {
                Console.WriteLine($"Customer said: {message}");
            });

            await _connection.StartAsync();
        }
    }
}
