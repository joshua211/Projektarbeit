using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Exceptions;
using EventFlow.ValueObjects;

namespace HT.Core.Shared.ValueObjects
{
    public class User : ValueObject
    {
        public User(Guid id, string displayName, List<Role> roles)
        {
            if (!roles.Any())
                throw DomainError.With("User has to be part of at least one role");
            if (id == Guid.Empty || string.IsNullOrEmpty(displayName))
                throw DomainError.With("Id and display name cant be empty", id, displayName);

            Id = id;
            DisplayName = displayName;
            this.Roles = roles;
        }

        public Guid Id { get; private set; }
        public string DisplayName { get; private set; }
        public List<Role> Roles { get; private set; }


        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Id };
    }
}