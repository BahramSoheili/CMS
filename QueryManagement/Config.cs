using QueryManagement.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QueryManagement
{
    public static class Config
    {
        public static void AddConfig(this IServiceCollection services
            , IConfiguration config)
        {
            services.AddElasticsearch(config);
            services.AddConfigQuery();
        }     
    }
}