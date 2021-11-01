using System;
namespace Core.Events
{
    public class EventMetaData
    {
        public Guid Id { get; set; }
        public bool PendingLockerLock { get; set; }
        public bool PendingFace { get; set; }
        public bool Deleted { get; set; }
        public DateTime LastUpdatedTimeStamp { get; set; }
        public DateTime LastUpdatedTimeStampLockerLock { get; set; }
        public DateTime LastUpdatedTimeStampFace { get; set; }
        public DateTime Created { get; set; }
        public string description { get; set; }
    }
}
