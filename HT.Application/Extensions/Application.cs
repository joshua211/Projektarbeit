using EventFlow;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using HT.Application.Article;
using HT.Application.Documents;
using HT.Application.Userdata;

namespace HT.Application.Extensions
{
    public static class Application
    {
        public static IEventFlowOptions AddApplication(this IEventFlowOptions options)
        {
            return options
                .AddDefaults(typeof(Application).Assembly)
                .UseMongoDbReadModel<DocumentReadModel>()
                .UseMongoDbReadModel<UserdataReadModel>()
                .UseMongoDbReadModel<ArticleReadModel>();
        }
    }
}