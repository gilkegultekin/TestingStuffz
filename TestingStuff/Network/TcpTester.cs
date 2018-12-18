using System;
using System.Threading.Tasks;

namespace TestingStuff.Network
{
    public class TcpTester : IDisposable
    {
        private readonly TcpSender _tcpSender;
        private readonly TcpReceiver _tcpReceiver;
        private const string Host = "127.0.0.1";
        private const int Port = 36666;
        private const char Delimiter = ';';
        private const char EndOfMessage = '!';

        public TcpTester()
        {
            _tcpSender = new TcpSender(Host, Port, Delimiter);
            _tcpReceiver = new TcpReceiver(Host, Port, Delimiter, EndOfMessage);
        }

        public void Initialize()
        {
            _tcpSender.Initialize();
            _tcpReceiver.Initialize();
        }

        public async Task DoYourThing()
        {
            //await Task.Run(() => { _tcpReceiver.Start(); });

            _tcpReceiver.Start();

            var receiverTask = Task.Run(async () =>
            {
                try
                {
                    await _tcpReceiver.StartListening();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
            
            var senderTask = Task.Run(async () =>
            {
                try
                {
                    await _tcpSender.Start();

                    var counter = 0;
                    while (_tcpSender.State)
                    {
                        await Task.Delay(3000);
                        var message = $"Message Number {++counter}";
                        await _tcpSender.SendMessage(message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            var stopperTask = Task.Run(async () =>
            {
                await Task.Delay(30000);
                _tcpSender.Stop();
                _tcpReceiver.Stop();
            });

            await Task.WhenAll(senderTask, receiverTask, stopperTask);
        }

        public void Dispose()
        {
            _tcpSender?.Dispose();
        }
    }
}
