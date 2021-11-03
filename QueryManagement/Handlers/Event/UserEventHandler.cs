using Core.Events;
using Core.Storage;
using QueryManagement;
using QueryManagement.Roles.Events.Created;
using QueryManagement.Roles.Events.Deleted;
using QueryManagement.Roles.Events.Updated;
using System.Threading;
using System.Threading.Tasks;

namespace QueryManagement.Handlers.Events
{
    internal class UserEventHandler :
        IEventHandler<UserCreated>,
        IEventHandler<UserUpdated>,
        IEventHandler<UserDeleted>
    {
        private readonly IRepository<User> repository;

        public UserEventHandler(IRepository<User> repository)
        {
            this.repository = repository;
        }
        public Task Handle(UserCreated @event, CancellationToken cancellationToken)
        {
            var document = new User(@event.Id, @event.IdCMS, @event.Data, @event.Created);
            return repository.Add(document, cancellationToken);
        }
        public Task Handle(UserUpdated @event, CancellationToken cancellationToken)
        {
            var document = repository.FindById(@event.Id, cancellationToken).Result;
            document.Update(@event.Id, @event.IdCMS, @event.Data,
                @event.LastUpdatedTimeStamp, @event.Created);
            return repository.Update(document, cancellationToken);
        }
        public Task Handle(UserDeleted @event, CancellationToken cancellationToken)
        {
            var document = repository.FindById(@event.Id, cancellationToken).Result;
            return repository.Delete(document, cancellationToken);
        }       
    }
}
