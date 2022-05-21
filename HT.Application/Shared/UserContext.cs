using System;
using System.Collections.Generic;
using System.Linq;
using HT.Application.Userdata;
using HT.Core.Shared.ValueObjects;

namespace HT.Application.Shared
{
    public class UserContext
    {
        public UserContext(Guid id, string displayName, List<string> roles)
        {
            Id = id;
            DisplayName = displayName;
            Roles = roles;
        }

        public Guid Id { get; private set; }
        public string DisplayName { get; private set; }
        public List<string> Roles { get; private set; }
        public UserdataReadModel Userdata { get; set; }

        public bool IsEditor => IsAdmin || IsInRole(Core.Shared.Roles.DocumentWriter);
        public bool IsAdmin => IsInRole(Core.Shared.Roles.Admin);

        public bool IsInRole(Role role) => Roles.Contains(role.Name);
        public bool IsInAnyRole(IEnumerable<string> roles) => Roles.Any(roles.Contains);

        public static UserContext FromUser(User user) => new UserContext(user.Id, user.DisplayName,
            user.Roles.Select(r => r.Name).ToList());

        public static User ToUser(UserContext context) => new User(context.Id, context.DisplayName,
            context.Roles.Select(n => new Role(n)).ToList());
    }
}