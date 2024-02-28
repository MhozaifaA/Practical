using Microsoft.Extensions.DependencyInjection;
//using Meteors.AspNetCore.Service.BoundedContext.Store;



namespace Meteors
{
    /// <summary>
    ///  Extension methods for add MrRepository Service
    /// </summary>
    public static class MrRepositoryServiceCollectionExtensions
    {


        public static IServiceCollection AddMrRepository(this IServiceCollection services, Action<MrRepositoryOptions> options)
        {
            services.Configure(options);
            MrRepositoryOptions _options = new MrRepositoryOptions();
             options(_options);
          //  _AddMrRepsitoryInject(services, _options.Assemblies.ToArray());
            return services;
        }


        
    }
}
