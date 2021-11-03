using System;
namespace Core.Events
{
    public class EventMetaData
    {
        public Guid Id { get; set; }
        public int IdCMS { get; set; }
        public bool Deleted { get; set; }
        public DateTime LastUpdatedTimeStamp { get; set; }
        public DateTime Created { get; set; }
        public string description { get; set; }
    }
}
