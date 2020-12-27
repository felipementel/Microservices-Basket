using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SportStore.Microservice.Basket.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Microservice.Basket.MessageBroker
{
    public class AzureServiceBusQueue : IMessageBroker
    {
        private readonly string _connectionString;

        private readonly IConfiguration _configuration;

        private readonly string TopicName = "itemBasket";
        static ITopicClient topicClient;

        private readonly ILogger<AzureServiceBusQueue> _logger;

        public AzureServiceBusQueue(
            IConfiguration configuration,
            ILogger<AzureServiceBusQueue> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration["AzureServiceBus"];
            topicClient = new TopicClient(_connectionString, TopicName);
        }

        public bool EnQueue<T>(T command, string queueName)
        {
            var message = JsonConvert
                .SerializeObject(
                command, 
                Formatting.Indented,
                new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            SendMessageAsync(message, queueName).Wait();

            return true;
        }

        private async Task SendMessageAsync(string messageBody, string queueName)
        {
            //var queueClient = new QueueClient(_connectionString, queueName);
            //var encodedMessage = new Message(Encoding.UTF8.GetBytes(message));
            //await queueClient.SendAsync(encodedMessage);
            //await queueClient.CloseAsync();

            var message = new Message(
                Encoding
                .UTF8
                .GetBytes(messageBody));

            // Write the body of the message to the console.
            _logger.LogDebug($"Sending message: {messageBody}");

            // Send the message to the topic.
            await topicClient.SendAsync(message);

            await topicClient.CloseAsync();
        }
    }
}
