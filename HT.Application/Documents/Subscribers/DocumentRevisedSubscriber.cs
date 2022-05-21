using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;
using HT.Application.Shared;
using HT.Application.Userdata.Queries;
using HT.Core.Aggregates.Document;
using HT.Core.Aggregates.Document.Events;

namespace HT.Application.Documents.Subscribers
{
    //TODO make this asynchronous
    public class DocumentRevisedSubscriber: ISubscribeSynchronousTo<DocumentAggregate, DocumentId, DocumentRevised>
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly INotificationService service;

        public DocumentRevisedSubscriber(IQueryProcessor queryProcessor, INotificationService service)
        {
            this.queryProcessor = queryProcessor;
            this.service = service;
        }

        public async Task HandleAsync(IDomainEvent<DocumentAggregate, DocumentId, DocumentRevised> domainEvent, CancellationToken cancellationToken)
        {
            var subscribedUsers = await queryProcessor.ProcessAsync(new GetUsersSubscribedToDocumentQuery(domainEvent.AggregateIdentity) ,cancellationToken);
            var editorId = domainEvent.AggregateEvent.Revision.User.Id;
            
            var tasks = subscribedUsers.Where(u => u.UserId != editorId).Select(userData =>
                service.NotifyDocumentRevisedAsync(userData.UserId, domainEvent.AggregateIdentity));

            await Task.WhenAll(tasks);
        }
    }
}