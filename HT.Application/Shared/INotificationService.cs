using System;
using System.Threading.Tasks;
using HT.Core.Aggregates.Document;

namespace HT.Application.Shared
{
    public interface INotificationService
    {
        Task NotifyDocumentRevisedAsync(Guid userId, DocumentId id);
    }
}