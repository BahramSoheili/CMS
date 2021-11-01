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
    public class UserQueryHandler: IQueryHandler<SearchUserById, UserView>


    {
        private readonly IDocumentSession session;
        public UserQueryHandler(IDocumentSession session)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }     
        public Task<UserView> Handle(SearchUserById request, CancellationToken cancellationToken)
        {
            return session.LoadAsync<UserView>(request.Id, cancellationToken);
        }    
    }
}
