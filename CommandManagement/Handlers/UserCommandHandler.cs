using Core.Commands;
using Core.Storage;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandManagement.Commands;
using System.Text;

namespace CommandManagement.Handlers
{
    class UserCommandHandler:
        ICommandHandler<CreateUser>,
        ICommandHandler<UpdateUser>,
        ICommandHandler<DeleteUser>
    {
        private readonly IRepository<User> repository;
        public UserCommandHandler(
            IRepository<User> repository
        )
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));;
        }
        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = User.Create(request.Id, request.IdCMS, request.Data);
            await repository.Add(user, cancellationToken);
            return Unit.Value;
        }
        public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await repository.FindById(request.Id, cancellationToken);
            user.Update(request.Id, user.IdCMS, request.Data);
            await repository.Update(user, cancellationToken);
            return Unit.Value;
        }
        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await repository.FindById(request.Id, cancellationToken);
            user.UpdateDeleted(request.Id, user.IdCMS, user.Data, user.Created);
            await repository.Update(user, cancellationToken);
            return Unit.Value;
        }      

    }
}
