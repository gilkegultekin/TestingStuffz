using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestingStuff.Network
{
    public class TcpSender : IDisposable
    {
        private readonly string _host;
        private readonly int _port;
        private readonly char _delimiter;
        private TcpClient _tcpClient;

        private NetworkStream _networkStream;

        public bool State { get; private set; }

        public TcpSender(string host, int port, char delimiter)
        {
            _host = host;
            _port = port;
            _delimiter = delimiter;
        }

        public void Initialize()
        {
            _tcpClient = new TcpClient();
        }

        public async Task Start()
        {
            await _tcpClient.ConnectAsync(_host, _port);
            _networkStream = _tcpClient.GetStream();
            State = true;
        }

        public async Task SendMessage(string message)
        {
            var messageAsBytes = Encoding.UTF8.GetBytes(message + _delimiter);

            await _networkStream.WriteAsync(messageAsBytes, 0, messageAsBytes.Length);

            Console.WriteLine($"TcpSender - Message sent! {message}");
        }

        public void Stop()
        {
            State = false;
            Console.WriteLine("Stopped TcpSender");
        }

        public void Dispose()
        {
            _tcpClient?.Dispose();
            _networkStream?.Dispose();
        }
    }
}
