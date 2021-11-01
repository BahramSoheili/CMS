using Core.Events;
using LibCMS.ValueObjects;
using System;

namespace QueryManagement.Roles.Events.Deleted
{
    public class UserDeleted : EventMetaData, IEvent
    {
        public Guid UserId { get; }
        public UserInfo Data { get; }
        public UserDeleted(Guid userId, UserInfo data)
        {
            UserId = userId;
            Data = data;
        }
        public static UserDeleted Create(Guid userId, UserInfo data)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"{nameof(userId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"Data can't be empty.");

            return new UserDeleted(userId, data);
        }
    }
}
