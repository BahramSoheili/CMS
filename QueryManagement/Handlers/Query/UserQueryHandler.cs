using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Storage;
using QueryManagement.Queries;

namespace QueryManagement.Handlers.Queries
{
    internal class UserQueryHandler :
        IQueryHandler<GetAllUsers, IReadOnlyCollection<User>>,
        IQueryHandler<SearchUserById, User>
    {

        private readonly Nest.IElasticClient elasticClient;
        private const int MaxItemsCount = 1000;
        private readonly IRepository<User> repository;
        public UserQueryHandler(Nest.IElasticClient elasticClient, 
                                IRepository<User> repository)
        {
            this.elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<IReadOnlyCollection<User>> Handle(GetAllUsers query, CancellationToken cancellationToken)
        { 
            return new ReadOnlyCollection<User>(await repository.GetAll()); 
        }
        public async Task<User> Handle(SearchUserById request, CancellationToken cancellationToken)
        {
           return await FindByCompanyId(request.IdCMS, cancellationToken);
        }
        public Task<User> FindByCompanyId(int idCMS, CancellationToken cancellationToken)
        {
            var response = elasticClient.SearchAsync<User>(
              s => s.Query(q => q
                      .Match(m => m
                        .Field(f => f.IdCMS).Query(idCMS.ToString())))).Result;
            var res = response.Documents;
            List<User> users = new List<User>(res);
            if (users.Count == 1)
            {
                return Task.FromResult(users[0]);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}

