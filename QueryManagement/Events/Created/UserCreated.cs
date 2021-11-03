using Core.Events;
using LibCMS.ValueObjects;
using System;

namespace QueryManagement.Roles.Events.Created
{
    internal class UserCreated : EventMetaData, IEvent
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
    }
}
