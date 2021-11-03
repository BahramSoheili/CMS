using Core.Events;
using LibCMS.ValueObjects;
using System;

namespace CommandManagement.Events
{
    public class UserDeleted : EventMetaData, IExternalEvent
    {
        public UserInfo Data { get; }
        public UserDeleted(Guid id, int idCMS, bool delted, UserInfo data,
            DateTime lastUpdatedTimeStamp, DateTime created)
        {
            Id = id;
            IdCMS = idCMS;
            Deleted = delted;
            Data = data;
            LastUpdatedTimeStamp = lastUpdatedTimeStamp;
            Created = created;
        }
        public static UserDeleted Create(Guid id, int idCMS, bool deleted,
            UserInfo data, DateTime lastUpdatedTimeStamp, DateTime created)
        {
            if (id == default(Guid))
                throw new ArgumentException($"{nameof(id)} needs to be defined.");

            if (created == default(DateTime))
                throw new ArgumentException($"{nameof(created)} needs to be defined.");
            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new UserDeleted(id, idCMS, deleted, data, lastUpdatedTimeStamp, created);
        }
    }
}

