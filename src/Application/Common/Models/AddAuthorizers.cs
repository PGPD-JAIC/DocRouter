using DocRouter.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DocRouter.Application.Common.Models
{
    /// <summary>
    /// Static class that adds authorization handlers from the assembly.
    /// </summary>
    public static class AddAuthorizers
    {
        /// <summary>
        /// Method that adds Authorizers from the project's assembly.
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> object.</param>
        /// <param name="assembly">The <see cref="Assembly"/></param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/></param>
        public static void AddAuthorizersFromAssembly(
                this IServiceCollection services,
                Assembly assembly,
                ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var authorizerType = typeof(IAuthorizer<>);
            assembly.GetTypesAssignableTo(authorizerType).ForEach((type) =>
            {
                foreach (var implementedInterface in type.ImplementedInterfaces)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(implementedInterface, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(implementedInterface, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(implementedInterface, type);
                            break;
                    }
                }
            });
        }
        /// <summary>
        /// Retrieves the types.
        /// </summary>
        /// <param name="assembly">The assembly containing the authorization handlers</param>
        /// <param name="compareType">The type being compared.</param>
        /// <returns></returns>
        public static List<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
        {
            var typeInfoList = assembly.DefinedTypes.Where(x => x.IsClass
                                && !x.IsAbstract
                                && x != compareType
                                && x.GetInterfaces()
                                        .Any(i => i.IsGenericType
                                                && i.GetGenericTypeDefinition() == compareType))?.ToList();

            return typeInfoList;
        }
    }
}
