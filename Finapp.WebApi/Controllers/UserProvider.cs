using Finapp.Database;
using Microsoft.AspNetCore.Http;

namespace Finapp.WebApi.Controllers
{
    internal static class UserProvider
    {
        public static FinappUser GetCurrentUser(HttpContext httpContext)
        {
            return (FinappUser) httpContext.Items["User"];
        }
    }
}