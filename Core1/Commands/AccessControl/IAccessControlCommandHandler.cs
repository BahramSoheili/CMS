using MediatR;

namespace Core.Commands
{
    public interface IAccessControlCommandHandler<in T>: IRequestHandler<T>
        where T : ICommand
    {
    }
}
