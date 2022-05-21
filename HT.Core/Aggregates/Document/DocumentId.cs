using EventFlow.Core;

namespace HT.Core.Aggregates.Document
{
    public class DocumentId : Identity<DocumentId>
    {
        public DocumentId(string value) : base(value)
        {
        }
    }
}