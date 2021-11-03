using Core.Commands;
using LibCMS.ValueObjects;
using System;

namespace CommandManagement.Commands
{
    public class UpdateUser: ICommand
    {
        public Guid Id { get; }
        public int IdCMS { get; }
        public UserInfo Data { get; }
        public UpdateUser(Guid id, int idCMS, UserInfo data)
        {
            Id = id;
            IdCMS = idCMS;
            Data = data;
        }
    }
}
