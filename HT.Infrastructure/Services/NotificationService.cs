using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HT.Application.Documents.Interfaces;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;

namespace HT.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration configuration;
        private readonly IDocumentService documentService;
        private readonly IGraphService graphService;

        public NotificationService(IDocumentService documentService, IGraphService graphService, IConfiguration configuration)
        {
            this.documentService = documentService;
            this.graphService = graphService;
            this.configuration = configuration;
        }

        public async Task NotifyDocumentRevisedAsync(Guid userId, DocumentId id)
        {
            var catalogId = configuration.GetSection("TeamsApp")["catalogId"];
            var result = await documentService.GetDocumentAsync(id, CancellationToken.None);
            var document = result.WasSuccessful
                ? result.Value
                : throw new Exception("Could not get the required document for notification");

            var topic = new TeamworkActivityTopic()
            {
                Source = TeamworkActivityTopicSource.EntityUrl,
                Value = $"https://graph.microsoft.com/v1.0/appCatalogs/teamsApps/{catalogId}"
            };
            var preview = new ItemBody()
            {
                Content = "Dokument wurde aktualisiert"
            };

            var templateParameters = new System.Collections.Generic.List<KeyValuePair>()
            {
                new()
                {
                    Name = "documentTitle",
                    Value = document.Revisions.Last().Title
                }
            };

            await graphService.SendNotificationAsync(userId, topic, "documentUpdated", preview, templateParameters);
        }
    }
}