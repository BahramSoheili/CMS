using System.Threading.Tasks;
using Core.Events.External;
using MediatR;

namespace Core.Events
{
    public class EventRabbitMqBus : IEventRabbitMqBus
    {
        private readonly IMediator mediator;
        private readonly IExternalRabbitMqEventProducer producer;

        public EventRabbitMqBus(
            IMediator mediator,
            IExternalRabbitMqEventProducer producer
        )
        {
            this.mediator = mediator;
            this.producer = producer;
        }

        public async Task Publish(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                await mediator.Publish(@event);

                if (@event is IExternalEvent externalEvent)
                    await producer.Publish(externalEvent);
            }
        }
    }
}
