using Core.Events;
using LibCMS.ValueObjects;
using System;

namespace CommandManagement.Events
{
    public class UserCreated : EventMetaData, IExternalEvent
    {
        public UserInfo Data { get; }

        public UserCreated(Guid id, int idCMS, UserInfo data,
            DateTime created)
        {
            Id = id;
            IdCMS = idCMS;
            Data = data;
            Created = created;
        }
        public static UserCreated Create(Guid userId, int idCMS,
            UserInfo data, DateTime created)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"{nameof(userId)} needs to be defined.");
             if (data == null)
                throw new ArgumentException($"data can't be empty.");
            if (created == default(DateTime))
                throw new ArgumentException($"{nameof(created)} needs to be defined.");
            return new UserCreated(userId, idCMS, data, created);
        }
    }
}

