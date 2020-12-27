namespace SportStore.Microservice.Basket.Domain.MessageBroker
{
    public interface IMessageBroker
    {
        bool EnQueue<T>(T command, string queueName);
    }
}
