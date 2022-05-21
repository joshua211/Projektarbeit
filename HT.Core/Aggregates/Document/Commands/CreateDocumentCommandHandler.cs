using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Document.Commands
{
    public class
        CreateDocumentCommandHandler : CommandHandler<DocumentAggregate, DocumentId, IExecutionResult,
            CreateDocumentCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(DocumentAggregate aggregate,
            CreateDocumentCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.CreateDocument(command.InitialRevision, command.Visibility,
                command.Creator, command.Roles,
                command.Category));
        }
    }
}