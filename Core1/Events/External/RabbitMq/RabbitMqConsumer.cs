using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Core.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Collections.Generic;

namespace Core.Events.External.Kafka
{
    public class RabbitMqConsumer: IExternalRabbitMqEventConsumer
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<RabbitMqConsumer> logger;
        private readonly KafkaConsumerConfig config;
        private const string UserName = "organisation-stsl";
        private const string Password = "caf96963b471bdb17e2f41f152db149d";
        private const string HostName = "events.blubug.net";
        private const string hostName = "localhost";
        ConnectionFactory connectionFactory;
        IConnection connection;
        private EventingBasicConsumer consumer;
        public RabbitMqConsumer(IServiceProvider serviceProvider,
            ILogger<RabbitMqConsumer> logger, IConfiguration configuration)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            // get configuration from appsettings.json
            config = configuration.GetKafkaConsumerConfig();
            //rabbitMq
            connectionFactory = new ConnectionFactory
            {
                HostName = hostName//,
                //UserName = UserName,v
                //Password = Password,
            };
            connection = connectionFactory.CreateConnection();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "Simulator",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var eventType = TypeProvider.GetTypeFromAnyReferencingAssembly("MessageCreatedBoard");
                        await sendEvent(message, eventType);
                    };
                    channel.BasicConsume(queue: "Simulator",
                                         autoAck: true,
                                         consumer: consumer);
            }
            catch (Exception e)
            {

            }
            logger.LogInformation("Kafka consumer started");
            // create consumer
            using (var consumer = new ConsumerBuilder<string, string>(config.ConsumerConfig).Build())
            {
                // subscribe to Kafka topics (taken from config)
                consumer.Subscribe(config.Topics);
                try
                {
                    // keep consumer working until it get signal that it should be shuted down
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        // consume event from Kafka
                        await ConsumeNextEvent(consumer, cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    logger.LogInformation("Error consuming message: " + e.Message + e.StackTrace);

                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    consumer.Close();
                }
            }
        }

        private async Task sendEvent(string message, Type eventType1)
        {
            var messagesJson = JsonConvert.DeserializeObject<MessageInfo[]>(message.ToString());
            foreach (var messageJson in messagesJson)
            {
                var deserilizeJson = JsonConvert.SerializeObject(messageJson);
                message = "{\"data\":" + deserilizeJson + "}";
                var @event = JsonConvert.DeserializeObject(message.ToString(), eventType1);
                using (var scope = serviceProvider.CreateScope())
                {
                    try
                    {
                        var eventBus = scope.ServiceProvider.GetRequiredService<IEventRabbitMqBus>();
                        // publish event to internal event bus
                        await eventBus.Publish(@event as IEvent);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }          
        }
        private string formatMessage(string message)
        {
            var content1 = message.Substring(1);
            var content2 = content1.Substring(0, content1.Length - 1);
            return content2;
        }
        public async Task StartAsync1(CancellationToken cancellationToken)
        {
            try
            {
                var channel = connection.CreateModel();
                channel.BasicQos(0, 5, false);
                consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    channel.BasicAck(ea.DeliveryTag, false);
                            // var message = Encoding.UTF8.GetString(body);
                            var eventType = TypeProvider
                     .GetTypeFromAnyReferencingAssembly("MessageCreatedBoard");
                            //var content1 = message.Substring(1);
                            //var content2 = content1.Substring(0, content1.Length - 1);
                            string message = @"[
            { ""ReadingVdd"": 3.177, ""OriginAddress"": 41101, ""ServerTimestamp"": ""2020-12-12T17: 38:15.669394"", ""BoundaryAddress"": 41101},
            { ""AuditTypeCode"":2, ""AuditSubject"": ""50dbacf500000000"", ""OriginAddress"": 42083, ""ServerTimestamp"": ""2020-12-12T17:38:15.668769"", ""ReadingSeqno"": 172241, ""BoundaryAddress"": 41101, ""OriginTimestamp"": ""2020-12-12T17:38:06.000000"",
            ""OriginInterval"": 0.0},
            { ""AuditTypeCode"":6, ""AuditSubject"": ""50dbacf500000000"", ""OriginAddress"": 42083, ""ServerTimestamp"":""2020-12-12T17: 38:15.669117"", ""ReadingSeqno"": 172242, ""BoundaryAddress"": 41101, ""OriginTimestamp"": ""2020-12-12T17:38:10.000000"", 
            ""OriginInterval"": 0.0} ]";
                            // var @event = JsonConvert.DeserializeObject(convertToField(message), eventType);
                            var @event = JsonConvert.DeserializeObject("{\"data\":" + message + "}", eventType);

                    using (var scope = serviceProvider.CreateScope())
                    {
                        try
                        {
                            var eventBus = scope.ServiceProvider.GetRequiredService<IEventRabbitMqBus>();
                                    // publish event to internal event bus
                                    await eventBus.Publish(@event as IEvent);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    channel.BasicConsume("readings-stsl", false, consumer);
                };
            }
            catch (Exception e)
            {

            }
            logger.LogInformation("Kafka consumer started");
            // create consumer
            using (var consumer = new ConsumerBuilder<string, string>(config.ConsumerConfig).Build())
            {
                // subscribe to Kafka topics (taken from config)
                consumer.Subscribe(config.Topics);
                try
                {
                    // keep consumer working until it get signal that it should be shuted down
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        // consume event from Kafka
                        await ConsumeNextEvent(consumer, cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    logger.LogInformation("Error consuming message: " + e.Message + e.StackTrace);

                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    consumer.Close();
                }
            }
        }

        private string convertToField(string message)
        { 
            var content1 = message.Substring(1);
            var content2 = content1.Substring(0, content1.Length - 1);
            var content3 = content2.Replace("Origin/Address", "originAddress");
            var content4 = content3.Replace("Boundary/Address", "boundaryAddress");
            var content5 = content4.Replace("Origin/Timestamp", "originTimestamp");
            var content6 = content5.Replace("Server/Timestamp", "serverTimestamp");
            var content7 = content6.Replace("Audit/Subject", "auditSubject");
            var content = "{\"data\":" + content7 + "}";
            return content;
        }
        private async Task ConsumeNextEvent(IConsumer<string, string> consumer, CancellationToken cancellationToken)
        {
            try
            {
                //lol ^_^ - remove this hack when this GH issue is solved: https://github.com/dotnet/extensions/issues/2149#issuecomment-518709751
                await Task.Yield();
                // wait for the upcoming message, consume it when arrives
                var message = consumer.Consume(cancellationToken);
                // get event type from name storred in message.Key
                var eventType = TypeProvider.GetTypeFromAnyReferencingAssembly(message.Key);
                // deserialize event
                var @event = JsonConvert.DeserializeObject(message.Value, eventType);
                using (var scope = serviceProvider.CreateScope())
                {
                    var eventBus = scope.ServiceProvider.GetRequiredService<IEventRabbitMqBus>();
                    // publish event to internal event bus
                    await eventBus.Publish(@event as IEvent);
                }
            }
            catch (Exception e)
            {
                logger.LogInformation("Error consuming message: " + e.Message + e.StackTrace);
            }
        }
    }
    public class MessageInfo
    {
        
        public decimal ReadingVdd { get; set; }
        public int AuditTypeCode { get; set; }
        public string AuditSubject { get; set; }
        public int OriginAddress { get; set; }
        public string ServerTimestamp { get; set; }
        public string ReadingSeqno { get; set; }
        public int BoundaryAddress { get; set; }
        public string OriginTimestamp { get; set; }
        public decimal OriginInterval { get; set; }
    }
}
