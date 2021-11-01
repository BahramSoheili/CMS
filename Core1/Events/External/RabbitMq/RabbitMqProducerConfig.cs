using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Core.Events.External.Kafka
{
    public class RabbitMqProducerConfig
    {
        public ProducerConfig ProducerConfig { get; set; }
        public List<string> Topics { get; set; }
    }

    public static class RabbitMqProducerConfigExtensions
    {
        public const string DefaultConfigKey = "KafkaProducer";

        public static RabbitMqProducerConfig GetRabbitMqProducerConfig(this IConfiguration configuration)
        {
            return configuration.GetSection(DefaultConfigKey).Get<RabbitMqProducerConfig>();
        }
    }
}
