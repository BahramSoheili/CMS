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
        public User(Guid id, int idCMS, UserInfo data, DateTime created)
        {
            Id = id;
            IdCMS = idCMS;
            Data = data;
            Created = created;
        }
        public void Update(Guid id, int idCMS, UserInfo data,
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
