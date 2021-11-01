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
        public static User Create(Guid id, UserInfo data)
        {
            return new User(id, data, DateTime.UtcNow);
        }
        public User(Guid id, UserInfo data, DateTime created)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = UserCreated.Create(id, data, created);
            Enqueue(@event);
            Apply(@event);
        }
        public void Update(Guid id, UserInfo data)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = UserUpdated.Create(id, data, DateTime.Now, this.Created);
            Enqueue(@event);
            Apply(@event);
        }
        public void Apply(UserCreated @event)
        {
            Id = @event.Id;
            Data = @event.Data;
            Created = @event.Created;
        }

        private void Apply(UserUpdated @event)
        {
            Id = @event.Id;
            Data = @event.Data;
            LastUpdatedTimeStamp = @event.LastUpdatedTimeStamp;
            Created = @event.Created;
        }

        internal void Deleted(Guid id)
        {
            throw new NotImplementedException();
        }
      
    }
}

