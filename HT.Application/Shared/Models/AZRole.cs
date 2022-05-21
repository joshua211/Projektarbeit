using System;

namespace HT.Application.Shared.Models
{
    public class AZRole
    {
        public AZRole(string displayName, Guid id)
        {
            DisplayName = displayName;
            Id = id;
        }

        public string DisplayName { get; private set; }
        public Guid Id { get; private set; }
    }
}