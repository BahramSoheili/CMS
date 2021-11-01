using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Storage;
using QueryManagement.Queries;

//using TextmagicRest;


namespace QueryManagement.Handlers.Queries
{
    internal class UserQueryHandler :
        IQueryHandler<GetAllUsers, IReadOnlyCollection<User>>,
        IQueryHandler<SearchUserById, User>
    {
        private const int MaxItemsCount = 1000;
        private readonly IRepository<User> repository;
        private readonly IQueryBus _queryBus;
        private int twoFactorsAuthentication;
        public UserQueryHandler(
            
            IRepository<User> repository,
            IQueryBus queryBus
        )
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _queryBus = queryBus;
        }
        public async Task<IReadOnlyCollection<User>> Handle(GetAllUsers query, CancellationToken cancellationToken)
        {
            //var users = await repository.GetAll<List<User>>(cancellationToken);//   FindById(request.Id, cancellationToken);
            // users;
            return null;

        }
        public async Task<User> Handle(SearchUserById request, CancellationToken cancellationToken)
        {
            var user = await repository.FindById(request.Id, cancellationToken);
            return user;
        }    
    }
}

