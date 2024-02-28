using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors
{
    public static class MrDbContextServiceCollectionExtensions
    {

        public static IServiceCollection AddMrDbContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : DbContext
          => services.AddDbContext<TContext>(optionsAction, contextLifetime, optionsLifetime).AddMrHttpResolverService();


        public static IServiceCollection AddMrDbContextPool<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction, int poolSize = 128) where TContext : DbContext
        => services.AddDbContextPool<TContext>(optionsAction, poolSize).AddMrHttpResolverService();


        public static IServiceCollection AddMrDbContextPool<TContext>(this IServiceCollection services, Action<IServiceProvider,DbContextOptionsBuilder> optionsAction, int poolSize = 128) where TContext : DbContext
        => services.AddDbContextPool<TContext>(optionsAction, poolSize).AddMrHttpResolverService();


        public static IServiceCollection AddMrDbContextFactory<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null, ServiceLifetime lifetime = ServiceLifetime.Singleton) where TContext : DbContext
        => services.AddMrDbContextFactory<TContext>(optionsAction, lifetime).AddMrHttpResolverService();



        public static IServiceCollection AddPooledMrDbContextFactory<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction, int poolSize = 128) where TContext : DbContext
        => services.AddPooledDbContextFactory<TContext>(optionsAction, poolSize).AddMrHttpResolverService();


        public static IServiceCollection AddPooledMrDbContextFactory<TContext>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, int poolSize = 128) where TContext : DbContext
        => services.AddPooledDbContextFactory<TContext>(optionsAction, poolSize).AddMrHttpResolverService();


    }
}
