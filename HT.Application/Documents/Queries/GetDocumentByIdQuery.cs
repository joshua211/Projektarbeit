using EventFlow.Queries;
using HT.Core.Aggregates.Document;

namespace HT.Application.Documents.Queries
{
    public class GetDocumentByIdQuery : IQuery<DocumentReadModel>
    {
        public DocumentId Id { get; private set; }

        public GetDocumentByIdQuery(DocumentId id)
        {
            Id = id;
        }
    }
}