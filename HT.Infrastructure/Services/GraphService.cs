using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HT.Application.Shared;
using HT.Application.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using KeyValuePair = Microsoft.Graph.KeyValuePair;

namespace HT.Infrastructure.Services
{
    public class GraphService : IGraphService
    {
        private readonly IConfiguration configuration;
        private readonly IContextProvider contextProvider;

        public GraphService(IContextProvider contextProvider, IConfiguration configuration)
        {
            this.contextProvider = contextProvider;
            this.configuration = configuration;
        }

        public async Task<UserContext> GetUserContextAsync(CancellationToken token = default)
        {
            var provider = contextProvider.AuthProvider;
            var client = new GraphServiceClient(provider);
            var me = await client.Me.Request().GetAsync(token);

            var enterpriseId = configuration.GetSection("TeamsApp")["enterpriseId"];

            var assignedRoles = (await client.Me.AppRoleAssignments.Request()
                    .Filter($"resourceId eq {enterpriseId}").GetAsync(token))
                .Where(asr => asr.AppRoleId != Guid.Empty);

            var appRoles = await GetAllRolesAsync(token);
            var roles = assignedRoles.Select<AppRoleAssignment, string>(a =>
                appRoles.First(ap => ap.Id == a.AppRoleId).DisplayName);

            return new UserContext(Guid.Parse(me.Id), me.DisplayName, roles.ToList());
        }

        public async Task<IEnumerable<AZRole>> GetAllRolesAsync(CancellationToken token = default)
        {
            var provider = contextProvider.AuthProvider;
            var client = new GraphServiceClient(provider);
            var objectId = configuration.GetSection("TeamsApp")["objectId"];
            var app = await client.Applications[objectId].Request().GetAsync(token);

            return app.AppRoles.Select(a => new AZRole(a.DisplayName, a.Id.GetValueOrDefault()));
        }

        public async Task SendNotificationAsync(Guid userId, TeamworkActivityTopic topic, string actType,
            ItemBody preview, List<KeyValuePair> templateParams)
        {
            var provider = contextProvider.AuthProvider;
            var client = new GraphServiceClient(provider);

            await client.Users[userId.ToString()].Teamwork
                .SendActivityNotification(topic, actType, null, preview, templateParams).Request().PostAsync();
        }
    }
}