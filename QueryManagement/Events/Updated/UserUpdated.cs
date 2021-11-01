using Core.Events;
using LibCMS.ValueObjects;
using System;
using System.Collections.Generic;

namespace QueryManagement.Roles.Events.Updated
{
    public class UserUpdated : EventMetaData, IEvent
    {
        public Guid UserId { get; }
        public UserInfo Data { get; }
        public UserUpdated(Guid userId, UserInfo data)
        {
            UserId = userId;
            Data = data;
        }
        public static UserUpdated Create(Guid userId, UserInfo data)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"{nameof(userId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"Data can't be empty.");

            return new UserUpdated(userId, data);
        }
    }
}
