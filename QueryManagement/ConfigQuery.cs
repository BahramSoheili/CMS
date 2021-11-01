using QueryManagement;
using QueryManagement.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Storage;
using MediatR;
using System.Collections.Generic;
using QueryManagement.Queries;
using QueryManagement.Handlers.Queries;
using QueryManagement.Handlers.Events;
using QueryManagement.Roles.Events.Created;
using QueryManagement.Roles.Events.Updated;

namespace QueryManagement
{
    public static class ConfigQuery
    {
        public static void AddConfigQuery(this IServiceCollection services)
        {
           services.AddConfigUser();
        }
        public static void AddConfigUser(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, ElasticSearchRepository<User>>();
            services.AddScoped<IRequestHandler<GetAllUsers, IReadOnlyCollection<User>>, UserQueryHandler>();
            services.AddScoped<INotificationHandler<UserCreated>, UserEventHandler>();
            services.AddScoped<INotificationHandler<UserUpdated>, UserEventHandler>();
            services.AddScoped<IRequestHandler<SearchUserById, User>, UserQueryHandler>();

        }
    }
}