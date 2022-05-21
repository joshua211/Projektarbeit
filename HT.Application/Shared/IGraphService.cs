using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HT.Application.Shared.Models;
using Microsoft.Graph;
using KeyValuePair = Microsoft.Graph.KeyValuePair;

namespace HT.Application.Shared
{
    public interface IGraphService
    {
        Task<UserContext> GetUserContextAsync(CancellationToken token = default);

        Task<IEnumerable<AZRole>> GetAllRolesAsync(CancellationToken token = default);

        Task SendNotificationAsync(Guid userId, TeamworkActivityTopic topic, string actType, ItemBody preview,
            List<KeyValuePair> templateParams);
    }
}