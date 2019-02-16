using Orleans.Configuration;
using Orleans.TutorialOne.GrainInterfaces;
using System.Threading.Tasks;

namespace Orleans.LiveAgent.WebApi.Orleans
{
    public class OrleansClient
    {
        private IClusterClient _client;

        public async Task ConnectClient()
        {
            if (_client != null && _client.IsInitialized)
            {
                return;
            }

            _client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansTutorialOne";
                })
                .Build();

            await _client.Connect();
            //Console.WriteLine("Client successfully connected to silo host \n");
        }

        public async Task SendMessage(string message)
        {
            var liveAgent = _client.GetGrain<ILiveAgent>(0);
            await liveAgent.ReceiveMessage(message);
        }
    }
}
