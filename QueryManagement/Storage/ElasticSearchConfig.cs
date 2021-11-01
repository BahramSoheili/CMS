using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace QueryManagement.Storage
{
    public static class ElasticSearchConfig
    {
        public static ElasticClient client;
        public static Nest.ConnectionSettings settings;
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            settings = new ConnectionSettings();
            services.AddUserIndex(configuration);   
            client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
        } 
        private static void AddUserIndex(this IServiceCollection services,
           IConfiguration configuration)
        {
            string Index = configuration["elasticUsers:index"];           

            settings.DefaultMappingFor<User>(i => i
                .IndexName(Index)
                .IdProperty(p => p.Id)
                .PropertyName(p => p.Data, "data"))
                .EnableDebugMode()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));         
        }

    
    }
}
