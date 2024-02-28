using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Meteors
{
    /// <summary>
    /// Merge common services to Domain to inject web conf
    /// </summary>
    public static class CommonServiceCollectionExtensions
    {
        /// <summary>
        /// Inject with helper <see cref="IHttpContextAccessor" />
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMrHttpResolverService(this IServiceCollection services)
            => services.AddHttpContextAccessor().AddTransient<IHttpResolverService, HttpResolverService>();


        /// <summary>
        /// Inject with helper <see cref="IHttpContextAccessor" />
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void TryAddMrHttpResolverService(this IServiceCollection services)
            => services.AddHttpContextAccessor().TryAddTransient<IHttpResolverService, HttpResolverService>();
    }
}
