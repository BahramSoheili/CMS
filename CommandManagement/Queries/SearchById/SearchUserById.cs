using CommandManagement.Views;
using Core.Queries;
using System;

namespace CommandManagement.Queries.SearchById
{
    public class SearchUserById : IQuery<UserView>
    {
        public Guid Id { get; }

        public SearchUserById(Guid id)
        {
            Id = id;
        }
    }
}
