using RabbitMQ.Client;
using RabbitMQ.Core.Interfaces;
using RabbitMQ.Core.Logging;
using RabbitMQ.Core.Utility;
using System;
using System.Text;

namespace RabbitMQ.Core.Senders
{
    public class WorkQueueSender : IInitializable, IMessageSender, IDisposable
    {
        private IConnection _connection;
        private IModel _channel;
        private const string ChannelName = Constants.WorkQueueChannelName;
        private readonly SimpleConsoleLogger<WorkQueueSender> _logger = new SimpleConsoleLogger<WorkQueueSender>();

        public void Initialize()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: ChannelName, durable: true, exclusive: false, autoDelete: false,
                arguments: null);

            _logger.Log($"The queue {ChannelName} has been declared!");
        }

        public void Send(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: "", routingKey: ChannelName, basicProperties: properties, body: body);

            _logger.Log($"Sent {message}");
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}
