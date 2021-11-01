using Core.Events;
using LibCMS.ValueObjects;
using System;

namespace QueryManagement.Roles.Events.Created
{
    internal class UserCreated : EventMetaData, IEvent
    {
        public Guid UserId { get; }
        public UserInfo Data { get; }
        public UserCreated(Guid userId, UserInfo data)
        {
            UserId = userId;
            Data = data;
        }
    }
}
