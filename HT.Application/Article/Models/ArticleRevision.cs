using System;
using HT.Application.Shared;

namespace HT.Application.Article.Models
{
    public class ArticleRevision
    {
        public ArticleRevision(UserContext userContext, DateTime date, string content, string title)
        {
            UserContext = userContext;
            Date = date;
            Content = content;
            Title = title;
        }

        public UserContext UserContext { get; private set; }
        public DateTime Date { get; private set; }
        public string Content { get; private set; }
        public string Title { get; private set; }
    }
}