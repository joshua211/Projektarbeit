using System.Collections.Generic;

namespace HT.Web.Shared
{
    public interface ISearchableContent<T>
    {
        IEnumerable<string> GetSearchableKeywords();
        T GetContent();
    }
}