using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Aggregates;
using Core.Events;
using Core.Storage;
using Marten;

namespace CommandManagement.Storage
{
    public class MartenRepository<T> : IRepository<T> where T : class, IAggregate, new()
    {
        private readonly IDocumentSession documentSession;
        private readonly IEventBus eventBus;
        public MartenRepository(IDocumentSession documentSession,
            IEventBus eventBus)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task<T> FindById(Guid id, CancellationToken cancellationToken)
        {
            var res =  documentSession.Events.AggregateStreamAsync<T>(id);
            return res;
        }
        public Task Add(T aggregate, CancellationToken cancellationToken)
        {
            documentSession.Events.StartStream<T>(aggregate.Id);
            return Store(aggregate, cancellationToken);
        }
        public Task Update(T aggregate, CancellationToken cancellationToken)
        {
            return Store(aggregate, cancellationToken);
        }
        public Task Delete(T aggregate, CancellationToken cancellationToken)
        {
            return Store(aggregate, cancellationToken);
        }
        private async Task Store(T aggregate, CancellationToken cancellationToken)
        {
            try
            {
                var events = aggregate.DequeueUncommittedEvents();
                documentSession.Events.Append(
                    aggregate.Id,
                    events.ToArray()
                );
                await documentSession.SaveChangesAsync();
                await eventBus.Publish(events.ToArray());
            }
            catch (Exception e)
            {
            }
           
        }
        public Task<T> Authenticate(string username, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            var res = documentSession.Query<T>().ToArray().ToList();
            return Task.FromResult(res);
        }

        //public Task<T> Search(Guid filter, CancellationToken cancellationToken)
        //{
        //    var res = documentSession.Events.AggregateStreamAsync<T>(filter);
        //    return res;
        //}
        public async Task SearchAll(string filter, CancellationToken cancellationToken)
        {
            var res = documentSession.Events.FetchStream(filter);
        }   
        public Task<T> LockerByRowCol(Guid wallId, string row, string col, CancellationToken cancellationToken) 
        {
            var res = documentSession.Query<T>("where data ->> 'Data.wallId' = 'wallId' && 'Data.rowNumber' = 'row' && 'Data.columnNumber' = 'col'").FirstOrDefault();
            return Task.FromResult(res);
        }

        public Task<List<T>> Search(string filter, CancellationToken cancellationToken)
        {
            var res = documentSession.Events.AggregateStreamAsync<List<T>>(filter);
            return res;
        }
    }
}
