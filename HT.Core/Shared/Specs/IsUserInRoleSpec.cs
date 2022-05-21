using System.Collections.Generic;
using EventFlow.Specifications;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Shared.Specs
{
    public class IsUserInRoleSpec : ISpecification<User>
    {
        private readonly Role role;

        private IsUserInRoleSpec(Role role)
        {
            this.role = role;
        }

        public bool IsSatisfiedBy(User user)
        {
            return user.IsUserInRole(role);
        }

        public IEnumerable<string> WhyIsNotSatisfiedBy(User user)
        {
            if (IsSatisfiedBy(user))
                yield break;
            yield return "User is not part of role " + role.Name;
        }

        public static IsUserInRoleSpec With(Role role) => new IsUserInRoleSpec(role);
    }
}