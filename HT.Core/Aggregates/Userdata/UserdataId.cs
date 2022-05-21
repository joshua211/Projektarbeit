using EventFlow.Core;

namespace HT.Core.Aggregates.Userdata
{
    public class UserdataId : Identity<UserdataId>
    {
        public UserdataId(string value) : base(value)
        {
        }
    }
}