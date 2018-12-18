using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestingStuff.Network
{
    public class TcpReceiver
    {
        private readonly string _host;
        private readonly int _port;
        private readonly char _delimiter;
        private readonly char _endOfMessage;

        private TcpListener _tcpListener;
        private const int BufferLength = 1024;

        private readonly ManualResetEventSlim _signal = new ManualResetEventSlim(false);


        public bool State { get; private set; }

        public TcpReceiver(string host, int port, char delimiter, char endOfMessage)
        {
            _host = host;
            _port = port;
            _delimiter = delimiter;
            _endOfMessage = endOfMessage;
        }

        public void Initialize()
        {
            _tcpListener = new TcpListener(IPAddress.Parse(_host), _port);
        }

        public async Task StartListening()
        {
            try
            {
                while (State)
                {
                    var client = await _tcpListener.AcceptTcpClientAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    Task.Run(async () =>
                    {
                        try
                        {
                            using (client)
                            {
                                await Read(client);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    });
                }
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
            catch (SocketException e)
            {
                Console.WriteLine($"{e.Message} - {e.SocketErrorCode}");
            }
            catch (ObjectDisposedException)
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _signal.Set();
            }
        }

        private async Task Read(TcpClient client)
        {
            var endOfMessage = false;
            var lastDelimiterIndex = -1;
            var message = "";

            using (var stream = client.GetStream())
            {
                while (!endOfMessage)
                {
                    await Task.Delay(10000);

                    var buffer = new byte[BufferLength];

                    var byteCount = await stream.ReadAsync(buffer, 0, BufferLength);
                    if (byteCount == 0)
                        continue;

                    var newMessage = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    message += newMessage;

                    var indexOfNextLimiter = message.IndexOf(_delimiter, lastDelimiterIndex + 1);
                    while (indexOfNextLimiter > -1)
                    {
                        var currentMessage = message.Substring(lastDelimiterIndex + 1,
                            indexOfNextLimiter - lastDelimiterIndex - 1);
                        Console.WriteLine($"TcpReceiver - Received the following message - {currentMessage}");

                        lastDelimiterIndex = indexOfNextLimiter;
                        indexOfNextLimiter = message.IndexOf(_delimiter, lastDelimiterIndex + 1);
                    }

                    if (newMessage.IndexOf(_endOfMessage) > -1)
                    {
                        endOfMessage = true;
                    }
                }
            }
        }

        public void Start()
        {
            State = true;
            _tcpListener.Start();
        }

        public void Stop()
        {
            State = false;
            _tcpListener.Server.Shutdown(SocketShutdown.Receive);
            _signal.Wait();
            _tcpListener.Stop(); //disposes socket
            Console.WriteLine("Stopped TcpReceiver");
        }
    }
}
