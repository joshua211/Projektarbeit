using System.Threading.Tasks;

namespace HT.Web.Services
{
    public interface IUserPreferenceService
    {
        Task<int> GetDocumentLayoutIdAsync();
        Task SetDocumentLayoutIdAsync(int id);
    }
}