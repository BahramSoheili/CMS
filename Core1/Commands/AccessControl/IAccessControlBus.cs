using System.Threading.Tasks;

namespace Core.Commands
{
    public interface IACBus
    { 
        Task Send<TCommand>(TCommand command) where TCommand: ICommand;
    }
}
