using Microsoft.Graph;

namespace HT.Application.Shared
{
    public interface IContextProvider
    {
        bool UserContextHasBeenSet { get; }

        IAuthenticationProvider AuthProvider { get; set; }
        UserContext GetUserContext();
        void SetUserContext(UserContext context);
    }
}