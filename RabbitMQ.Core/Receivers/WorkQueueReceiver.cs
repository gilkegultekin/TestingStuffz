using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Core.Interfaces;
using RabbitMQ.Core.Logging;
using RabbitMQ.Core.Utility;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Receivers
{
    public class WorkQueueReceiver : IInitializable, IMessageReceiver, IDisposable
    {
        private IConnection _connection;
        private IModel _channel;
        private const string ChannelName = Constants.WorkQueueChannelName;
        private readonly SimpleConsoleLogger<WorkQueueReceiver> _logger = new SimpleConsoleLogger<WorkQueueReceiver>();

        public void Initialize()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: ChannelName, durable: true, exclusive: false, autoDelete: false,
                arguments: null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false); //don't want to get messages until I finish processing the current message, fetch messages one by one

            _logger.Log($"The queue \"{ChannelName}\" has been declared!");
        }

        public void StartReceiving()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                _logger.Log($"Received {message}");

                var dotCount = message.Split('.').Length - 1;
                await Task.Delay(dotCount * 1000);
                _logger.Log("Finished processing message");

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: ChannelName, autoAck: false, consumer: consumer);
            _logger.Log($"Started consuming the queue \"{ChannelName}\"");
        }

        public void StopReceiving()
        {
            _channel.Abort(1, "Stopped by client");
            _connection.Close(1, "Stopped by client");
            _logger.Log($"Stopped consuming the queue \"{ChannelName}\"");
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}
