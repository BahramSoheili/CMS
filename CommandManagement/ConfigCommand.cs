using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using CommandManagement.Storage;
using MediatR;
using Core.Storage;
using CommandManagement.Handlers;
using CommandManagement.Commands;
using CommandManagement.Queries.SearchById;
using CommandManagement.Views;

namespace CommandManagement
{
    public static class ConfigCommand
    {
        public static void AddConfigCommand(this IServiceCollection services)
        {
            services.AddUserScope();           
        }       
        public static void AddUserScope(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, MartenRepository<User>>();
            services.AddScoped<IRequestHandler<CreateUser, Unit>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUser, Unit>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUser, Unit>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<SearchUserById, UserView>, UserQueryHandler>();
        }
    }
}
