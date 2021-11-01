using Core.Commands;
using LibCMS.ValueObjects;
using System;

namespace CommandManagement.Commands
{
    public class UpdateUser: ICommand
    {
        public Guid Id { get; }
        public UserInfo Data { get; }
        public UpdateUser(Guid id, UserInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
