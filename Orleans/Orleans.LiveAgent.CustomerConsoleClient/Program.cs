using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Orleans.LiveAgent.CustomerConsoleClient
{
    class Program
    {
        private static HubConnection _connection;

        private static readonly HttpClient _client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Customer Console Client...");
            DoWork().Wait();
        }

        private static async Task DoWork()
        {
            await ConnectToHub();

            while (true)
            {
                var msg = Console.ReadLine();
                var request =
                    new HttpRequestMessage(HttpMethod.Post, $"http://localhost:52484/api/values/SendToGrain?message={msg}")
                    {
                        Content = new StringContent("")
                    };
                await _client.SendAsync(request);
            }
        }

        private static async Task ConnectToHub()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:61746/customer")
                .Build();

            _connection.On<string>("ReceiveMessage", message =>
            {
                Console.WriteLine($"LiveAgent said: {message}");
            });

            await _connection.StartAsync();
        }
    }
}
