using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace GradeSystem.v1.Server.Providers
{
    public class EmailBasedUserIdProvider:IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Gender)?.Value;
        }
    }
}
