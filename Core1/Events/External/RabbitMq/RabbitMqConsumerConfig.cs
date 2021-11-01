using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Core.Events.External.Kafka
{
    public class RabbitMqConsumerConfig
    {
        public ConsumerConfig ConsumerConfig { get; set; }
        public string[] Topics { get; set; }
    }

    public static class RabbitMqConsumerConfigExtensions
    {
        public const string DefaultConfigKey = "KafkaConsumer";

        public static RabbitMqConsumerConfig GetRabbitMqConsumerConfig(this IConfiguration configuration)
        {
            return configuration.GetSection(DefaultConfigKey).Get<RabbitMqConsumerConfig>();
        }
    }
}
