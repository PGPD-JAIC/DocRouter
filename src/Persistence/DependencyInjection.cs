using DocRouter.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocRouter.Persistence
{
    /// <summary>
    /// Configures the service dependencies used in the Persistence layer
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures the service collection
        /// </summary>
        /// <param name="services">An implementation of <see cref="IServiceCollection"></see></param>
        /// <param name="configuration">An implementation of <see cref="IConfiguration"></see></param>
        /// <returns></returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DocRouterContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DocRouterDatabase")).EnableSensitiveDataLogging());
            services.AddScoped<IDocRouterContext>(provider => provider.GetService<DocRouterContext>());

            return services;
        }
    }
}
