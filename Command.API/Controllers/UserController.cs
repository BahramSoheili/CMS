using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Commands;
using Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using LibCMS.ValueObjects;
using CommandManagement.Commands;
using CommandManagement.Views;
using CommandManagement.Queries.SearchById;

namespace Command.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        public UsersController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInfo command)
        {          
                       
            return await sendUserCreate(command);             
            throw new Exception("Information is not valid.");
        }

        private async Task<IActionResult> sendUserCreate(UserInfo command)
        {
            var id = Guid.NewGuid();
            var document = new CreateUser(id, command);
            await _commandBus.Send(document);
            return Created("api/users", id);
        }      
       
        [AllowAnonymous]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody] UserInfo command)
        {
            var user = GetUser(userId);
            if (user != null)
            {              
                var document = new UpdateUser(userId, command);
                await _commandBus.Send(document);
                return Ok();            
            }
            throw new Exception("user is not exist or user information is not valid.");
        }
        [AllowAnonymous]
        private UserView GetUser(Guid Id)
        {
            return _queryBus.Send<SearchUserById, UserView>
                (new SearchUserById(Id)).Result;
        }

        [AllowAnonymous]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _commandBus.Send(new DeleteUser(userId));
            return Ok();
        }
    }
}