using System.Collections.Generic;
using EventFlow.Exceptions;
using EventFlow.ValueObjects;

namespace HT.Core.Shared.ValueObjects
{
    public class Role : ValueObject
    {
        public Role(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw DomainError.With("Role name cant be empty", name);
            this.Name = name;
        }

        public string Name { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}