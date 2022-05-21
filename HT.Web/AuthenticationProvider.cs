using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazorade.Teams.Model;
using Microsoft.Graph;

namespace HT.Web
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private ApplicationContext Context;

        public AuthenticationProvider(ApplicationContext context)
        {
            this.Context = context
                           ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var authResult = await this.Context.TeamsInterop.Authentication
                .AcquireTokenAsync(this.Context.Context.LoginHint);

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                authResult.AccessToken
            );
        }
    }
}