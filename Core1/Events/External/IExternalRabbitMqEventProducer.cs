using System.Threading.Tasks;

namespace Core.Events.External
{
    public interface IExternalRabbitMqEventProducer
    {
        Task Publish(IExternalEvent @event);
    }
}
