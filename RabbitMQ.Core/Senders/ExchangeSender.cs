using System;
using RabbitMQ.Client;
using RabbitMQ.Core.Interfaces;
using RabbitMQ.Core.Logging;
using RabbitMQ.Core.Utility;
using System.Text;

namespace RabbitMQ.Core.Senders
{
    public class ExchangeSender : IInitializable, IMessageSender, IDisposable
    {
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = Constants.PubSubDemoExchangeName;
        private readonly SimpleConsoleLogger<ExchangeSender> _logger = new SimpleConsoleLogger<ExchangeSender>();

        public void Initialize()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _channel.ExchangeDeclare(exchange: ExchangeName, type: "fanout");

            _logger.Log($"The exchange \"{ExchangeName}\" has been declared!");
        }

        public void Send(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: ExchangeName, routingKey: "", basicProperties: null, body: body);

            _logger.Log($"Sent {message}");
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}
