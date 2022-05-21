using System;
using EventFlow.Exceptions;
using EventFlow.ValueObjects;

namespace HT.Core.Shared.ValueObjects
{
    public class Revision : ValueObject
    {
        public Revision(User user, Title title, DateTime date, string content)
        {
            if (date == default(DateTime))
                throw DomainError.With($"Date {date} cant be null or default");
            if (string.IsNullOrEmpty(content))
                throw DomainError.With("Content cant be null");

            User = user ?? throw DomainError.With("User cant be null");
            Title = title ?? throw DomainError.With("Title cant be null");
            Date = date;
            Content = content;
        }

        public User User { get; private set; }
        public Title Title { get; private set; }
        public DateTime Date { get; private set; }
        public string Content { get; private set; }
    }
}