using Core.Events;
using LibCMS.ValueObjects;
using System;

namespace QueryManagement.Roles.Events.Deleted
{
    internal class UserDeleted : EventMetaData, IEvent
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
    }
}
