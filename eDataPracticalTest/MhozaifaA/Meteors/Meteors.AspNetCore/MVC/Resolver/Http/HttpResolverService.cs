using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Meteors
{
    /// <summary>
    /// Inject <see cref="IHttpContextAccessor"/> enabled resolver http and user to more specific
    /// </summary>
    public class HttpResolverService : IHttpResolverService
    {

        private readonly IHttpContextAccessor HttpContextAccessor;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public HttpResolverService(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
        }

        public HttpContext? HttpContext
            => HttpContextAccessor.HttpContext;

        //public string AppBaseUrl =>
        //    $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";

        public ClaimsPrincipal? User
         => HttpContextAccessor.HttpContext?.User;

     
        public string BaseUrl() => $"{HttpContext?.Request?.Scheme}://{HttpContext?.Request?.Host}{HttpContext?.Request?.PathBase}";

        public Tkey? GetCurrentUserId<Tkey>() where Tkey : struct, IEquatable<Tkey>
            => IsAuthenticated().NestedIF(() => HttpContext.User.CurrentUserId<Tkey>(),() => (Tkey?)null);

        public string GetCurrentUserName()
         => IsAuthenticated().NestedIF(() => HttpContext.User.Identity.Name, () => null);


        public bool IsAuthenticated()
            => (HttpContextAccessor?.HttpContext?.User is not null).NestedIF(() => (HttpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false), false);

    }
}
