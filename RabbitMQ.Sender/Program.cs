using RabbitMQ.Core.Senders;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkQueueSender();

            Console.ReadKey();
        }

        static void WorkQueueSender()
        {
            using (var workQueueSender = new WorkQueueSender())
            {
                workQueueSender.Initialize();

                var random = new Random(Guid.NewGuid().GetHashCode());
                for (int i = 0; i < 100; i++)
                {
                    var next = random.Next(0, 10);
                    Console.WriteLine($"Next random number is {next}! Generating message...");

                    var messageBuilder = new StringBuilder(next.ToString());
                    for (int j = 0; j < next; j++)
                    {
                        messageBuilder.Append(".");
                    }

                    workQueueSender.Send(messageBuilder.ToString());
                }

                Console.WriteLine("Press [enter] to exit");
                Console.ReadLine();
            }
        }

        static void SimpleSender()
        {
            using (var simpleSender = new SimpleSender())
            {
                simpleSender.Initialize();
                simpleSender.Send("Hello World");

                var task = Task.Run(async () =>
                {
                    for (int i = 1; i < 11; i++)
                    {
                        await Task.Delay(i * 300);
                        simpleSender.Send(i.ToString());
                    }
                });

                task.Wait();

                Console.WriteLine("Press [enter] to exit");
                Console.ReadLine();
            }
        }
    }
}
