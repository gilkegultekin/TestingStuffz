namespace RabbitMQ.Core.Interfaces
{
    public interface IMessageReceiver
    {
        void StartReceiving();

        void StopReceiving();
    }
}