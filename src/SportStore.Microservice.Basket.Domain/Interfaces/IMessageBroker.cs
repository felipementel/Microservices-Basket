namespace SportStore.Microservice.Basket.Domain.Interfaces
{
    public interface IMessageBroker
    {
        bool EnQueue<T>(T command, string queueName);
    }
}
