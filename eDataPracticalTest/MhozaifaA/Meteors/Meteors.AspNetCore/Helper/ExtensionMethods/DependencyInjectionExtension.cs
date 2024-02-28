using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Meteors
{
    public static class DependencyInjectionExtension
    {
        public static IServiceScope CreateFactoryScope(this IApplicationBuilder app)
       => app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        public static T GetProviderService<T>(this IServiceScope scop)
            => scop.ServiceProvider.GetService<T>();

        public static T GetProviderRequiredService<T>(this IServiceScope scop)
            => scop.ServiceProvider.GetRequiredService<T>();
    }
}
