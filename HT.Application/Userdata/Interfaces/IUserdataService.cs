using System.Threading;
using System.Threading.Tasks;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;
using HT.Core.Aggregates.Userdata;

namespace HT.Application.Userdata.Interfaces
{
    public interface IUserdataService
    {
        Task<ServiceResult<UserdataReadModel>> GetUserdataAsync(UserContext context, CancellationToken token);

        Task<ServiceResult<UserdataReadModel>> CreateUserdataAsync(UserContext context, CancellationToken token);

        Task<ServiceResult> SubscribeToDocumentAsync(UserdataId userdataId, DocumentId documentId, CancellationToken token);
        Task<ServiceResult> UnsubscribeToDocumentAsync(UserdataId userdataId, DocumentId documentId, CancellationToken token);
    }
}