using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aggregates;

namespace Core.Storage
{
    public interface IRepository<T> where T : IAggregate
    {
        Task<List<T>> GetAll(CancellationToken cancellationToken);

        Task  SearchAll(string filter, CancellationToken cancellationToken);
        Task<T> Search(string filter, CancellationToken cancellationToken);


        //Task<T> Search(Guid filter, CancellationToken cancellationToken);
        Task<T> FindById(Guid id, CancellationToken cancellationToken);
        Task<T> Authenticate(string username, string password, CancellationToken cancellationToken);

        Task Add(T aggregate, CancellationToken cancellationToken);

        Task Update(T aggregate, CancellationToken cancellationToken);

        Task Delete(T aggregate, CancellationToken cancellationToken);
        Task<T> LockerByRowCol(Guid wallId, string row, string col, CancellationToken cancellationToken);

    }
}
