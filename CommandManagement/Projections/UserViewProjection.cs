using CommandManagement.Events;
using CommandManagement.Views;
using Marten.Events.Projections;
using System;
namespace CommandManagement.Projections
{
    public class UserViewProjection : ViewProjection<UserView, Guid>
    {
        public UserViewProjection()
        {
            ProjectEvent<UserCreated>(e => e.Id, Apply);
            ProjectEvent<UserUpdated>(e => e.Id, Apply);
            ProjectEvent<UserDeleted>(e => e.Id, Apply);
        }
        private void Apply(UserView view, UserCreated @event)
        {
            view.Id = @event.Id;
            view.IdCMS = @event.IdCMS;
            view.Data = @event.Data;
            view.Created = @event.Created;
        }
        private void Apply(UserView view, UserUpdated @event)
        {
            view.Id = @event.Id;
            view.IdCMS = @event.IdCMS;
            view.Data = @event.Data;
            view.LastUpdatedTimeStamp = @event.LastUpdatedTimeStamp;
            view.Created = @event.Created;
        }
        private void Apply(UserView view, UserDeleted @event)
        {
            view.Id = @event.Id;
            view.IdCMS = @event.IdCMS;
            view.Deleted = @event.Deleted;
            view.Data = @event.Data;
            view.LastUpdatedTimeStamp = @event.LastUpdatedTimeStamp;
            view.Created = @event.Created;
        }
    }
}
