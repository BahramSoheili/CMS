using System.Threading.Tasks;
using MediatR;

namespace Core.Commands
{
    public class ACBus : IACBus
    {
        private readonly IMediator _mediator;

        public ACBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Send<TCommand>(TCommand command) where TCommand: ICommand
        {
            return _mediator.Send(command);
        }
    }
}
