using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Core.Interfaces;
using RabbitMQ.Core.Logging;
using RabbitMQ.Core.Utility;
using System;
using System.Text;

namespace RabbitMQ.Core.Receivers
{
    public class SimpleReceiver : IInitializable, IMessageReceiver, IDisposable
    {
        private IConnection _connection;
        private IModel _channel;
        private const string ChannelName = Constants.SimpleDemoChannelName;
        private readonly SimpleConsoleLogger<SimpleReceiver> _logger = new SimpleConsoleLogger<SimpleReceiver>();

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

        public void StartReceiving()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                _logger.Log($" [x] Received {message}");
            };

            _channel.BasicConsume(queue: ChannelName, autoAck: true, consumer: consumer);

            _logger.Log($"Started consuming the queue {ChannelName}");
        }

        public void StopReceiving()
        {
            _channel.Abort(1, "Stopped by client");
            _connection.Close(1, "Stopped by client");
            _logger.Log($"Stopped consuming the queue {ChannelName}");
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}
