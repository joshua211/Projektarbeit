using EventFlow.Exceptions;
using EventFlow.ValueObjects;

namespace HT.Core.Shared.ValueObjects
{
    public class Title : ValueObject
    {
        public Title(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw DomainError.With("Title cant be empty");
            Value = value;
        }

        public string Value { get; private set; }

        public static implicit operator string(Title title) => title.Value;
        public static implicit operator Title(string str) => new Title(str);
    }
}