using HT.Application.Shared;
using Microsoft.Graph;

namespace HT.Infrastructure.Services
{
    public class ContextProvider : IContextProvider
    {
        private UserContext context;

        public UserContext GetUserContext()
        {
            if (context is null)
                throw new System.Exception("User Context has not been set");

            return context;
        }

        public void SetUserContext(UserContext context)
        {
            this.context = context;
        }

        public IAuthenticationProvider AuthProvider { get; set; }

        public bool UserContextHasBeenSet => context is not null;
    }
}