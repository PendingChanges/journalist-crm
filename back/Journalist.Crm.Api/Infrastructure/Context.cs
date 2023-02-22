using Journalist.Crm.Domain;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Journalist.Crm.Api.Infrastructure
{
    public class Context : IContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Context(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "public";
    }
}
