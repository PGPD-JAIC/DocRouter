using DocRouter.Application.Common.Interfaces;
using DocRouter.Common;
using DocRouter.Infrastructure.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DocRouter.Infrastructure
{
    /// <summary>
    /// Implements dependency injection for the Infrastructure project.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<IFileStorageService, AzureADFileStorageService>();
            var emailSettings = new EmailSettings
            {
                EmailId = configuration["EmailSettings:EmailId"],
                Host = configuration["EmailSettings:Host"],
                Name = configuration["EmailSettings:Name"],
                Port = Convert.ToInt32(configuration["EmailSettings:Port"]),
                UseSSL = Convert.ToBoolean(configuration["EmailSettings:UseSSL"]),
                ApplicationBaseUrl = configuration["EmailSettings:ApplicationBaseUrl"]
            };
            services.AddSingleton(emailSettings);
            var azureAdSettings = new AzureADSettings
            {
                ClientId = configuration["AzureAD:ClientId"],
                TenantId = configuration["AzureAD:TenantId"],
                ClientSecret = configuration["AzureAD:ClientSecret"]

            };            
            services.AddSingleton(azureAdSettings);
            services.AddTransient<INotificationService, NotificationService>();
            services.AddScoped<IUserService, AzureADUserService>();
            return services;
        }
    }
}
