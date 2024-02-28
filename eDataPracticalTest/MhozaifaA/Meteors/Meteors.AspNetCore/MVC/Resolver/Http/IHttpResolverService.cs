using Microsoft.AspNetCore.Http;


namespace Meteors
{
    /// <summary>
    /// <para>resolver Interface wide use</para>
    /// Inject <see cref="HttpContextAccessor"/> enabled resolver http and user to more specific
    /// </summary>
    public interface IHttpResolverService
    {
        /// <summary>
        /// Encapsulates all HTTP-specific information about an individual HTTP request.
        /// </summary>
        HttpContext HttpContext { get; }

        /// <summary>
        /// Return user-id content in Authorize header by <see cref="MrClaimTypes.NameIdentifier"/>.
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <returns><see cref="string"/></returns>
        Tkey? GetCurrentUserId<Tkey>() where Tkey : struct, IEquatable<Tkey>;
       
        /// <summary>
        /// Check Authorize header  User?.Identity?.IsAuthenticated.
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        bool IsAuthenticated();
        string BaseUrl();
        string GetCurrentUserName();
    }
}
