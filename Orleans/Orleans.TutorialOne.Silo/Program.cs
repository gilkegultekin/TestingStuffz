using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.TutorialOne.Grains;
using System;
using System.Threading.Tasks;

namespace Orleans.TutorialOne.Silo
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
                var host = await StartSilo();
                Console.WriteLine("\n\n Press Enter to terminate...\n\n");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            //define the cluster config
            var builder = new SiloHostBuilder()
                .AddAdoNetGrainStorage("SqlStorage", options =>
                {
                    options.Invariant = "System.Data.SqlClient";
                    options.ConnectionString = "Server=localhost;Database=OrleansStorageTest;User=sa;Password=1q2w3e4r*;";
                    options.UseJsonFormat = true;
                })
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansTutorialOne";
                })
                .ConfigureApplicationParts(parts =>
                    parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
