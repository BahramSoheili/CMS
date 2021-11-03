using Core.Events;
using LibCMS.ValueObjects;
using System;

namespace CommandManagement.Events
{
    public class UserUpdated : EventMetaData, IExternalEvent
    {
        public UserInfo Data { get; }

        public UserUpdated(Guid id, int idCMS, UserInfo data, 
            DateTime lastUpdatedTimeStamp, DateTime created)
        {
            Id = id;
            IdCMS = idCMS;
            Data = data;
            LastUpdatedTimeStamp = lastUpdatedTimeStamp;
            Created = created;
        }
        public static UserUpdated Create(Guid userId, int idCMS, UserInfo data,
            DateTime lastUpdatedTimeStamp, DateTime created)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"{nameof(userId)} needs to be defined.");
            if (data == null)
                throw new ArgumentException($"data can't be empty.");
            if (lastUpdatedTimeStamp == default(DateTime))
                throw new ArgumentException($"{nameof(lastUpdatedTimeStamp)} needs to be defined.");
            return new UserUpdated(userId, idCMS, data, lastUpdatedTimeStamp, created);
        }
    }
}

