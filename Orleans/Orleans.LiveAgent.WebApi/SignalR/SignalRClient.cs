using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Orleans.LiveAgent.WebApi.SignalR
{
    public class SignalRClient
    {
        private HubConnection _connection;

        public async Task ConnectToHub()
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
                .WithUrl("http://localhost:61746/customer")
                .Build();

            await _connection.StartAsync();
        }

        public async Task SendMessage(string message)
        {
            await _connection.InvokeAsync("SendMessageToCustomer", message);
        }
    }
}
