using Core.Aggregates;
using CommandManagement.Events;
using System;
using System.Collections.Generic;
using LibCMS.ValueObjects;

namespace CommandManagement
{
    internal class User : Aggregate
    {
        public UserInfo Data { get; private set; }
        public User()
        {
        }
        public static User Create(Guid id, int idCMS, UserInfo data)
        {
            return new User(id, idCMS, data, DateTime.UtcNow);
        }
        public User(Guid id, int idCMS, UserInfo data, DateTime created)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = UserCreated.Create(id, idCMS, data, created);
            Enqueue(@event);
            Apply(@event);
        }
        public void Update(Guid id, int idCMS, UserInfo data)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = UserUpdated.Create(id, idCMS, data, DateTime.Now, this.Created);
            Enqueue(@event);
            Apply(@event);
        }
        public void Apply(UserCreated @event)
        {
            Id = @event.Id;
            IdCMS = @event.IdCMS;
            Data = @event.Data;
            Created = @event.Created;
        }
        public void Apply(UserUpdated @event)
        {
            Id = @event.Id;
            IdCMS = @event.IdCMS;
            Data = @event.Data;
            LastUpdatedTimeStamp = @event.LastUpdatedTimeStamp;
            Created = @event.Created;
        }  
        public void UpdateDeleted(Guid id, int idCMS, UserInfo data, DateTime created)
        {
            Delete(id, idCMS, data, created);
        }
        public void Delete(Guid id, int idCMS, UserInfo data, DateTime created)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = UserDeleted.Create(id, idCMS, true, data, DateTime.Now, this.Created);
            Enqueue(@event);
            Apply(@event);
        }
        public void Apply(UserDeleted @event)
        {
            Id = @event.Id;
            IdCMS = @event.IdCMS;
            Deleted = @event.Deleted;
            Data = @event.Data;
            LastUpdatedTimeStamp = @event.LastUpdatedTimeStamp;
            Created = @event.Created;
        }
    }
}

