using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Orleans.PoC.SignalRHost.Hubs
{
    public class CustomerHub : Hub
    {
        public async Task SendMessageToCustomer(string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message);
        }
    }
}
