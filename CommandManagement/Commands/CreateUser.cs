using Core.Commands;
using LibCMS.ValueObjects;
using System;
using System.Collections.Generic;

namespace CommandManagement.Commands
{
    public class CreateUser: ICommand
    {
        public Guid Id { get; }
        public UserInfo Data { get; }
        public CreateUser(Guid id, UserInfo data)
        {
             Id = id;
             Data = data;
        }
    }
}
