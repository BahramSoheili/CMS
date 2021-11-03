using Core.Events;
using LibCMS.ValueObjects;
using System;
using System.Collections.Generic;

namespace QueryManagement.Roles.Events.Updated
{
    internal class UserUpdated : EventMetaData, IEvent
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
    }
}
