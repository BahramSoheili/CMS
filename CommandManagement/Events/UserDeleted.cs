using Core.Events;
using LibCMS.ValueObjects;
using System;

namespace CommandManagement.Events
{
    public class UserDeleted : EventMetaData, IExternalEvent
    {
        public UserInfo Data { get; }
        public UserDeleted(Guid userId, UserInfo data, DateTime created)
        {
            Id = userId;
            Data = data;
            Created = created;
        }
        public static UserDeleted Create(Guid userId, UserInfo data, DateTime created)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"{nameof(userId)} needs to be defined.");

            if (created == default(DateTime))
                throw new ArgumentException($"{nameof(created)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new UserDeleted(userId, data, created);
        }
    }
}

