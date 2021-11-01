using System.Threading;
using System.Threading.Tasks;

namespace Core.Events.External
{
    public interface IExternalRabbitMqEventConsumer
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
