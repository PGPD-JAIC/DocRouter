using Azure.Identity;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocRouter.Infrastructure.Azure
{
    public class AzureADUserService : IUserService
    {
        private GraphServiceClient graphClient;
        private readonly ILogger _logger;
        public AzureADUserService(AzureAD settings, ILogger<AzureADUserService> logger)
        {
            var clientSecretCredential = new ClientSecretCredential(settings.TenantId, settings.ClientId, settings.ClientSecret);
            graphClient = new GraphServiceClient(clientSecretCredential);
            _logger = logger;
        }
        public async Task<List<DirectoryUser>> GetAllUsersAsync()
        {
            var result = await graphClient.Users.GetAsync();
            return result.Value.Select(u => new DirectoryUser { Email = u.Mail, Name = u.DisplayName }).ToList();
        }
    }
}
