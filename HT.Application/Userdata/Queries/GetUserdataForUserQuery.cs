using EventFlow.Queries;
using HT.Application.Shared;

namespace HT.Application.Userdata.Queries
{
    public class GetUserdataForUserQuery : IQuery<UserdataReadModel>
    {
        public GetUserdataForUserQuery(UserContext context)
        {
            Context = context;
        }

        public UserContext Context { get; private set; }
    }
}