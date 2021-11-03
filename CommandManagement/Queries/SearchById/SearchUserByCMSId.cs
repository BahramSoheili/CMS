using CommandManagement.Views;
using Core.Queries;
using System;

namespace CommandManagement.Queries.SearchById
{
    public class SearchUserByCMSId : IQuery<UserView>
    {
        public int IdCMS { get; }

        public SearchUserByCMSId(int idCMS)
        {
            IdCMS = idCMS;
        }
    }
}
