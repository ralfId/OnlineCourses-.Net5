using Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Security.UserSecurity
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetCurrentUser()
        {
            var userName = _contextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userName;
        }
    }
}
