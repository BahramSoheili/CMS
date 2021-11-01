using Core.Aggregates;
using LibCMS.ValueObjects;
using Newtonsoft.Json;
using System;

namespace QueryManagement
{
    public class User : Aggregate
    {
        public UserInfo Data { get; protected set; }
        public User()
        {
        }
        [JsonConstructor]
        public User(Guid id, UserInfo data, DateTime created)
        {
            Id = id;
            Data = data;
            Created = created;
        }
        public void Update(Guid id, UserInfo data,
            DateTime lastUpdatedTimeStamp, DateTime created)
        {
            Id = id;
            Data = data;
            LastUpdatedTimeStamp = lastUpdatedTimeStamp;
            Created = created;
        }
    }
}
