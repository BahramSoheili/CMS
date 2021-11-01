using System.Threading.Tasks;

namespace Core.Events
{
    public interface IEventRabbitMqBus
    {
        Task Publish(params IEvent[] events);
    }
}
