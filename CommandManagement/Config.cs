using Core.Storage;
using CommandManagement.Views;
using CommandManagement.Projections;
using CommandManagement.Storage;
using Marten;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CommandManagement.Commands;
using CommandManagement.Handlers;
using CommandManagement.Queries.SearchById;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System;

namespace CommandManagement
{
    public static class Config
    {
        public static string Url;
        public static void AddConfig(this IServiceCollection services,
             IConfiguration config)
        {
            services.AddMarten(config, options =>
            {
                ConfigCommand.ConfigureMarten(options);
            });
            Url = config["url:index"];
            services.AddConfigCommand();
        }       

        public static HttpContent ToJsonStringContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}
