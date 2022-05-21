using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using HT.Application.Article.Interfaces;
using HT.Application.Documents.Interfaces;
using HT.Application.Shared;
using HT.Application.Userdata.Interfaces;
using HT.Infrastructure.Services;

namespace HT.Infrastructure.Extensions
{
    public static class Infrastructure
    {
        public static IEventFlowOptions AddInfrastructure(this IEventFlowOptions options)
        {
            return options.AddQueryHandlers(typeof(Infrastructure).Assembly)
                .RegisterServices(r =>
                {
                    r.Register<IDocumentService, DocumentService>(Lifetime.AlwaysUnique);
                    r.Register<IArticleService, ArticleService>(Lifetime.AlwaysUnique);
                    r.Register<IUserdataService, UserdataService>(Lifetime.AlwaysUnique);
                    r.Register<IGraphService, GraphService>(Lifetime.AlwaysUnique);
                    r.Register<INotificationService, NotificationService>(Lifetime.AlwaysUnique);
                    r.Register<IContextProvider, ContextProvider>(Lifetime.Scoped);
                });
        }
    }
}