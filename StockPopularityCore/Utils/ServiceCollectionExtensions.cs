using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace StockPopularityCore.Utils
{
    public static class ServiceCollectionExtensions // TODO: Add to external library
    {
        private static void RegisterAllTypes<T>(this IServiceCollection services, IEnumerable<Assembly> assemblies,
                                                ServiceLifetime lifetime)
        {
            var typesFromAssemblies = assemblies
                                      .SelectMany(a => a.DefinedTypes
                                                        .Where(x => x.GetInterfaces().Contains(typeof(T)))
                                                        .Where(x => !x.IsAbstract)
                                                        .Where(x => !x.IsInterface))
                                      .ToArray();

            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }


        public static void RegisterAllTypesTransient<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.RegisterAllTypes<T>(assemblies, ServiceLifetime.Transient);
        }


        public static void RegisterAllTypesScoped<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.RegisterAllTypes<T>(assemblies, ServiceLifetime.Scoped);
        }


        public static void RegisterAllTypesSingleton<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.RegisterAllTypes<T>(assemblies, ServiceLifetime.Singleton);
        }
    }
}