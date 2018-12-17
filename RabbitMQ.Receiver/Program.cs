using RabbitMQ.Core.Receivers;
using System;
using System.Threading.Tasks;

namespace RabbitMQ.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            ExchangeReceiver();

            Console.ReadKey();
        }

        static void ExchangeReceiver()
        {
            using (var exchangeReceiver = new ExchangeReceiver())
            {
                exchangeReceiver.Initialize();
                exchangeReceiver.StartReceiving();

                Console.WriteLine("Press [enter] to exit");
                Console.ReadLine();
                exchangeReceiver.StopReceiving();
            }
        }

        static void WorkQueueReceiver()
        {
            using (var workQueueReceiver = new WorkQueueReceiver())
            {
                workQueueReceiver.Initialize();
                workQueueReceiver.StartReceiving();

                Console.WriteLine("Press [enter] to exit");
                Console.ReadLine();
                workQueueReceiver.StopReceiving();
            }
        }

        static void SimpleReceiver()
        {
            var simpleReceiver = new SimpleReceiver();

            var startReceiverTask = Task.Factory.StartNew(s =>
            {
                var receiver = (SimpleReceiver)s;

                receiver.Initialize();
                receiver.StartReceiving();
            }, simpleReceiver);

            startReceiverTask.Wait();

            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
            simpleReceiver.StopReceiving();
            simpleReceiver.Dispose();
        }
    }
}
