using CommandManagement.Views;
using Core.Queries;
using System;
using System.Collections.Generic;

namespace CommandManagement.Queries.SearchById
{
    public class GetAllUsers : IQuery<IReadOnlyCollection<UserView>>
    {
        public GetAllUsers()
        {
        }
    }
}
