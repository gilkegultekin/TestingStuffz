namespace RabbitMQ.Core.Interfaces
{
    public interface IMessageSender
    {
        void Send(string message);
    }
}