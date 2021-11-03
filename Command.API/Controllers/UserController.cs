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
            //var document = new CreateUser(id, 1, command); we might need it for the first time for avoiding empty table
            var document = new CreateUser(id, GetIdCMS(), command);
            await _commandBus.Send(document);
            return Created("api/users", id);
        }
        private int GetIdCMS() {
            var max = GetMaxIdCMS();
            return max++;
        }   
        private int GetMaxIdCMS()
        {
            if (IsUserExist())
            {
                return _queryBus.Send<SearchUserMaxCMSId, int>
                           (new SearchUserMaxCMSId()).Result;
            }
            else
            {
                return 1;
            }            
        }

        private bool IsUserExist()
        {
            return _queryBus.Send<GetAllUsers, IReadOnlyCollection<UserView>>
                                      (new GetAllUsers()).Result.Count > 0 ? true : false;
        }

        [AllowAnonymous]
        [HttpPut("{idCMS}")]
        public async Task<IActionResult> Put(int idCMS, [FromBody] UserInfo command)
        {
            var user = GetUser(idCMS);
            if (user != null)
            {              
                var document = new UpdateUser(user.Id, idCMS, command);
                await _commandBus.Send(document);
                return Ok();            
            }
            throw new Exception("user is not exist or user information is not valid.");
        }
        [AllowAnonymous]
        private UserView GetUser(int idCMS)
        {
            return _queryBus.Send<SearchUserByCMSId, UserView>
                (new SearchUserByCMSId(idCMS)).Result;
        }

        [AllowAnonymous]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int idCMS)
        {
            var user = GetUser(idCMS);
            if (user != null)
            {
                await _commandBus.Send(new DeleteUser(user.Id));
                return Ok();
            }
            throw new Exception("user is not exist or user information is not valid.");
        }
    }
}