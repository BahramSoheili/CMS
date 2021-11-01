using Core.Events;
using LibCMS.ValueObjects;

namespace CommandManagement.Views
{
    public class UserView: EventMetaData
    {
        public UserInfo Data { get; set; }
    }
}
