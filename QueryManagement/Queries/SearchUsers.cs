using Core.Queries;
using System.Collections.Generic;
namespace QueryManagement.Queries
{
    public class SearchUsers : IQuery<IReadOnlyCollection<User>>
    {
        public SearchUsers()
        {
        }
    }
}

