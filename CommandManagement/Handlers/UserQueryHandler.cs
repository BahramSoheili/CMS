using System.Linq;
using Core.Queries;
using Marten;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommandManagement.Views;
using CommandManagement.Queries;
using CommandManagement.Queries.SearchById;

namespace CommandManagement.Handlers
{
    public class UserQueryHandler:
        IQueryHandler<GetAllUsers, IReadOnlyCollection<UserView>>,
        IQueryHandler<SearchUserById, UserView>,
        IQueryHandler<SearchUserMaxCMSId, int>,
        IQueryHandler<SearchUserByCMSId, UserView>
    {
        private readonly IDocumentSession session;
        public UserQueryHandler(IDocumentSession session)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }
        public Task<IReadOnlyCollection<UserView>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            var res = session.Query<UserView>().ToList();
            IReadOnlyCollection<UserView> users = res.AsReadOnly();
            return Task.FromResult(users);
        }
        public Task<UserView> Handle(SearchUserById request, CancellationToken cancellationToken)
        {
            return session.LoadAsync<UserView>(request.Id, cancellationToken);
        }
        public Task<UserView> Handle(SearchUserByCMSId request, CancellationToken cancellationToken)
        {
            return Task.FromResult(session.Query<UserView>()
                .Where(l => l.IdCMS == request.IdCMS).ToList().FirstOrDefault());
        }
        public Task<int> Handle(SearchUserMaxCMSId request, CancellationToken cancellationToken)
        {
            //return Task.FromResult(5);
            return Task.FromResult(session.Query<UserView>()
               .Max(l => l.IdCMS));
        }
    }
}
