using System;
using RabbitMQ.Client;
using RabbitMQ.Core.Interfaces;
using RabbitMQ.Core.Utility;
using System.Text;
using RabbitMQ.Core.Logging;

namespace RabbitMQ.Core.Senders
{
    public class SimpleSender : IInitializable, IMessageSender, IDisposable
    {
        private IConnection _connection;
        private IModel _channel;
        private const string ChannelName = Constants.SimpleDemoChannelName;
        private readonly SimpleConsoleLogger<SimpleSender> _logger = new SimpleConsoleLogger<SimpleSender>();


        public void Initialize()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: ChannelName, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            _logger.Log($"The queue {ChannelName} has been declared!");
        }

        public void Send(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: ChannelName, basicProperties: null, body: body);

            _logger.Log($" [x] Sent {message}");
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}
