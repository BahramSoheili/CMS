using Core.Aggregates;
using Core.Events;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryManagement.Storage
{
    public class ElasticSearchRepository<T>: IRepository<T> where T : class, IAggregate, new()
    {
        private readonly Nest.IElasticClient elasticClient;
        private const int MaxItemsCount = 1000;
        public ElasticSearchRepository(Nest.IElasticClient elasticClient, IEventBus eventBus)
        {
            this.elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
            //this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            //this.repository = new ElasticsearchRepository(elasticClient);
        }
        public async Task<IReadOnlyCollection<T>> Filter(string filter, CancellationToken cancellationToken)
        {
            var response = await elasticClient.SearchAsync<T>(
                s => s.Query(q => q.QueryString(d => d.Query(filter))).Size(MaxItemsCount)
            );
            return response.Documents;
        }
        public async Task<T> FindById(Guid id, CancellationToken cancellationToken)
        {
            var response = await elasticClient.GetAsync<T>(id);
            return response.Source;
        }
        public Task Add(T aggregate, CancellationToken cancellationToken)
        {
            var res = elasticClient.IndexAsync(aggregate, i => i.Id(aggregate.Id));
            var result = res.Result;
            return res; 
        }
        public Task Update(T aggregate, CancellationToken cancellationToken)
        {
            return elasticClient.UpdateAsync<T>(aggregate.Id, i => i.Doc(aggregate));
        }
        public Task Delete(T aggregate, CancellationToken cancellationToken)
        {
            return elasticClient.DeleteAsync<T>(aggregate.Id);
        }      
        public Task<List<T>> Search(string filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<T> SearchByGuid(Guid filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            //List<string> indexedList = new List<string>();
            //var scanResults = elasticClient.Search<T>(s => s
            //                .From(0)
            //                .Size(MaxItemsCount)
            //                .MatchAll()
            //                //.Fields(f => f.Field(fi => fi.propertyName)) 
            //                //I used field to get only the value which needed rather than getting the whole document
            //                .SearchType(Elasticsearch.Net.SearchType.QueryThenFetch)
            //                .Scroll("5m")
            //            );

            //var results = elasticClient.Scroll<T>("10m", scanResults.ScrollId);
            //while (results.Documents.Any())
            //{
            //    foreach (var doc in results.Fields)
            //    {
            //        indexedList.Add(doc.Value<string>("propertyName"));
            //    }

            //    results = elasticClient.Scroll<T>("10m", results.ScrollId);
            //}
            throw new NotImplementedException();

        }
        public Task SearchAll(string filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

       
    }
}
