using Core.Commands;
using LibCMS.ValueObjects;
using System;
using System.Collections.Generic;

namespace CommandManagement.Commands
{
    public class CreateUser: ICommand
    {
        public Guid Id { get; }
        public int IdCMS { get; }
        public UserInfo Data { get; }
        public CreateUser(Guid id, int idCMS, UserInfo data)
        {
             Id = id;
             IdCMS = idCMS;
             Data = data;
        }
    }
}
