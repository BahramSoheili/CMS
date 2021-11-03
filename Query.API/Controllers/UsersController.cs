using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Queries;
using Microsoft.AspNetCore.Mvc;
using Core.Storage;
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
        public Task<User> SearchById(int Id)
        {
            return _queryBus.Send<SearchUserById, User>(new SearchUserById(Id));
        }
        [HttpGet]
        [Route("getAll/")]
        public Task<IReadOnlyCollection<User>> GetAll()
        {
            return _queryBus.Send<GetAllUsers, IReadOnlyCollection<User>>(new GetAllUsers());
        }
    }
}
