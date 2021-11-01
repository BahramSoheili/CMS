using Core.Commands;
using System;

namespace CommandManagement.Commands
{
    public class DeleteUser: ICommand
    {
        public Guid Id { get; }
        public DeleteUser(Guid id)
        {
            Id = id;
        }
    }
}
