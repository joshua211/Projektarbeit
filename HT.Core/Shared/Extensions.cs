using System.Linq;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Shared
{
    public static class Extensions
    {
        public static bool IsUserInRole(this User user, Role role)
        {
            return user.Roles.Any(r => r == role);
        }
    }
}