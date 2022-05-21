using System.Collections.Generic;
using System.Linq;
using HT.Application.Documents;

namespace HT.Web.Shared.Documents
{
    public class SearchableDocument : ISearchableContent<DocumentReadModel>
    {
        private readonly DocumentReadModel readModel;

        public SearchableDocument(DocumentReadModel readModel)
        {
            this.readModel = readModel;
        }
        
        public IEnumerable<string> GetSearchableKeywords()
        {
            return new[] { readModel.Category, readModel.Revisions.LastOrDefault()?.Title };
        }

        public DocumentReadModel GetContent()
        {
            return readModel;
        }
    }
}