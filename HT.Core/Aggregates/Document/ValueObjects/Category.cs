using EventFlow.Exceptions;
using EventFlow.ValueObjects;

namespace HT.Core.Aggregates.Document.ValueObjects
{
    public class Category : ValueObject
    {
        public Category(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw DomainError.With("Category name cant be empty", name);

            this.Name = name;
        }

        public string Name { get; private set; }
    }
}