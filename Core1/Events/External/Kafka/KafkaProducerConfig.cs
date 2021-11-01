using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Core.Events.External.Kafka
{
    public class KafkaProducerConfig
    {
        public ProducerConfig ProducerConfig { get; set; }
        public List<string> Topics { get; set; }
    }

    public static class KafkaProducerConfigExtensions
    {
        public const string DefaultConfigKey = "KafkaProducer";

        public static KafkaProducerConfig GetKafkaProducerConfig(this IConfiguration configuration)
        {
            return configuration.GetSection(DefaultConfigKey).Get<KafkaProducerConfig>();
        }
    }
}
