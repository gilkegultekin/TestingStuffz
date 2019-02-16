using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.LiveAgent.WebApi.Orleans;
using Orleans.LiveAgent.WebApi.SignalR;
using Orleans.TutorialOne.GrainInterfaces;

namespace Orleans.LiveAgent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly SignalRClient _signalRClient;
        private readonly OrleansClient _orleansClient;

        public ValuesController(SignalRClient signalRClient, OrleansClient orleansClient)
        {
            _signalRClient = signalRClient;
            _orleansClient = orleansClient;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("[action]")]
        public async Task SendToGrain([FromQuery] string message)
        {
            await _orleansClient.ConnectClient();
            await _orleansClient.SendMessage(message);

            //await _signalRClient.ConnectToHub();
            //await _signalRClient.SendMessage(message);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task SendToCustomer([FromQuery] string message)
        {
            await _signalRClient.ConnectToHub();
            await _signalRClient.SendMessage(message);
        }

        private async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;

            client = new ClientBuilder()
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
