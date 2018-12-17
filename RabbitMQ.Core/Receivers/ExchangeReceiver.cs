using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Core.Interfaces;
using RabbitMQ.Core.Logging;
using RabbitMQ.Core.Utility;
using System;
using System.Text;

namespace RabbitMQ.Core.Receivers
{
    public class ExchangeReceiver : IInitializable, IMessageReceiver, IDisposable
    {
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = Constants.PubSubDemoExchangeName;
        private string _queueName;
        private readonly SimpleConsoleLogger<ExchangeReceiver> _logger = new SimpleConsoleLogger<ExchangeReceiver>();

        public void Initialize()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: ExchangeName, type: "fanout");

            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: ExchangeName, routingKey: "");

            _logger.Log($"The exchange \"{ExchangeName}\" has been declared and an anonymous queue has been bound to it!");
        }

        public void StartReceiving()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                _logger.Log($"Received {message}");
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            _logger.Log($"Started consuming the queue \"{_queueName}\"");
        }

        public void StopReceiving()
        {
            _channel.Abort(1, "Stopped by client");
            _connection.Close(1, "Stopped by client");
            _logger.Log($"Stopped consuming the queue \"{_queueName}\"");
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}
