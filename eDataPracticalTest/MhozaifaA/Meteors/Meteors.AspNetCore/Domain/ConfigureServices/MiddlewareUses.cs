using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace Meteors
{
    /// <summary>
    /// Middleware configuration sqlserver  seed/db ..
    /// </summary>
    public static class MiddlewareUses
    {
        /// <summary>
        /// localy store until disposed <see cref="DisposeSqlServerSeed(IApplicationBuilder)"/>
        /// </summary>
        private static IServiceScope ServiceScope;

        /// <summary>
        /// app middleware used for call seed methods and pass context
        /// </summary>
        /// <typeparam name="T"> type of <see cref="DbContext"/> </typeparam>
        /// <param name="builder">  Defines a class that provides the mechanisms to configure an application's request
        ///     pipeline. </param>
        /// <param name="context"> as return scope inject of <see cref="DbContext"/></param>
        /// <returns> <see cref="IApplicationBuilder"/> </returns>
        public static IApplicationBuilder UseSqlServerSeed<T>(this IApplicationBuilder builder, Action<T> context) where T : DbContext
        {
            ServiceScope = builder.CreateFactoryScope();
            context(ServiceScope.GetProviderService<T>());
            return builder;
        }


        /// <summary>
        /// app middleware used for call seed methods and pass context
        /// </summary>
        /// <typeparam name="T"> type of <see cref="DbContext"/>  only to insure  </typeparam>
        /// <param name="builder">  Defines a class that provides the mechanisms to configure an application's request
        ///     pipeline. </param>
        /// <param name="context"> as return scope inject of <see cref="DbContext"/></param>
        /// <returns> <see cref="IApplicationBuilder"/> </returns>
        public static IApplicationBuilder UseSqlServerSeed<T>(this IApplicationBuilder builder, Action<IServiceProvider> context) where T : DbContext
        {
            ServiceScope = builder.CreateFactoryScope();
            context(ServiceScope.ServiceProvider);
            return builder;
        }


        /// <summary>
        /// app middleware used for call seed methods and pass context
        /// </summary>
        /// <typeparam name="T"> type of <see cref="DbContext"/> </typeparam>
        /// <param name="builder">  Defines a class that provides the mechanisms to configure an application's request
        ///     pipeline. </param>
        /// <param name="context"> as return scope inject of <see cref="DbContext"/></param>
        /// <returns> <see cref="IApplicationBuilder"/> </returns>
        public static IApplicationBuilder UseSqlServerSeed<T>(this IApplicationBuilder builder, Action<T, IServiceProvider> context) where T : DbContext
        {
            ServiceScope = builder.CreateFactoryScope();
            context(ServiceScope.GetProviderService<T>(), ServiceScope.ServiceProvider);
            return builder;
        }


        /// <summary>
        /// Last call to Dispose <see cref="IServiceScope"/>
        /// <para>It alternative of <see langword="using"/> </para>
        /// <para> Don't use with <see cref="Delegate"/> invoker </para>
        /// </summary>
        /// <param name="builder"></param>
        public static void DisposeSqlServerSeed(this IApplicationBuilder builder)
        =>  ServiceScope.Dispose();
    }
}
