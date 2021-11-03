using Core.Queries;
namespace QueryManagement.Queries
{
    public class SearchUserById: IQuery<User>
    {
        public int IdCMS { get; }

        public SearchUserById(int idCMS)
        {
            IdCMS = idCMS;
        }
    }
}

