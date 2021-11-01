using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Core.Storage;
using System.Threading;
using QueryManagement;
using QueryManagement.Queries;

namespace Query.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController: Controller
    {
        private readonly IQueryBus _queryBus;
        public UsersController(IQueryBus queryBus, IRepository<User> repository)
        {
            _queryBus = queryBus;
        }         
        [HttpGet("{Id}")]
        public Task<User> SearchById(Guid Id)
        {
            return _queryBus.Send<SearchUserById, User>(new SearchUserById(Id));
        }
        [HttpGet]
        [Route("getAll/")]
        public Task<IReadOnlyCollection<User>> GetAll()
        {
            return _queryBus.Send<SearchUsers, IReadOnlyCollection<User>>(new SearchUsers());
        }
    }
}
