using Microsoft.AspNetCore.SignalR;
using Orleans.Configuration;
using Orleans.TutorialOne.GrainInterfaces;
using System.Threading.Tasks;

namespace Orleans.PoC.SignalRHost.Hubs
{
    public class LiveAgentHub : Hub
    {
        public async Task SendMessageLiveAgentClient(string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToGrain(string message)
        {
            using (var client = await ConnectClient())
            {
                var liveAgent = client.GetGrain<ILiveAgent>(0);
                await liveAgent.SendMessage(message);
            }
        }

        private async Task<IClusterClient> ConnectClient()
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansTutorialOne";
                })
                .Build();

            await client.Connect();
            //Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }
    }
}
