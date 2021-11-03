using Core.Queries;
using System.Collections.Generic;

namespace QueryManagement.Queries
{
    public class GetAllUsers : IQuery<IReadOnlyCollection<User>>
    {
        public GetAllUsers()
        {
        }
    }
}


