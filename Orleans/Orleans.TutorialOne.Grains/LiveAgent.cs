using Microsoft.AspNetCore.SignalR.Client;
using Orleans.TutorialOne.GrainInterfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Orleans.TutorialOne.Grains
{
    public class LiveAgent : Grain, ILiveAgent
    {
        private HubConnection _connection;

        private readonly HttpClient _client = new HttpClient();

        public override async Task OnActivateAsync()
        {
            if (_connection != null && _connection.State == HubConnectionState.Connected)
            {
                return;
            }

            if (_connection != null)
            {
                await _connection.StartAsync();
                return;
            }

            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:61746/liveAgent")
                .Build();

            await _connection.StartAsync();

            await base.OnActivateAsync();
        }

        public async Task ReceiveMessage(string message)
        {
            //signalR üzerinden live agent console app'e mesajı yolla
            await _connection.InvokeAsync("SendMessageLiveAgentClient", message);
        }

        public async Task SendMessage(string message)
        {
            //web api ye live agent'ın mesajını http request ile yolla (SendToCustomer metodu)
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:52484/api/values/SendToCustomer?message={message}")
            {
                Content = new StringContent("")
            };

            await _client.SendAsync(request);
        }
    }
}
