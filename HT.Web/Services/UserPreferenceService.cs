using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace HT.Web.Services
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private const string DocumentLayoutId = "userDocumentLayoutId";

        private readonly ILocalStorageService localStorage;

        public UserPreferenceService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<int> GetDocumentLayoutIdAsync() => await localStorage.GetItemAsync<int>(DocumentLayoutId);

        public async Task SetDocumentLayoutIdAsync(int id) => await localStorage.SetItemAsync(DocumentLayoutId, id);
    }
}