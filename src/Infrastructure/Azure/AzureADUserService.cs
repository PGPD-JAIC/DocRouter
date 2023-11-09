using Azure.Identity;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocRouter.Infrastructure.Azure
{
    public class AzureADUserService : IUserService
    {
        private GraphServiceClient graphClient;
        private readonly AzureADSettings _azureAdSettings;
        private readonly ILogger _logger;
        public AzureADUserService(AzureADSettings settings, ILogger<AzureADUserService> logger)
        {
            var clientSecretCredential = new ClientSecretCredential(settings.TenantId, settings.ClientId, settings.ClientSecret);
            graphClient = new GraphServiceClient(clientSecretCredential);
            _logger = logger;
        }
        public async Task<List<DirectoryUser>> GetAllUsersAsync()
        {
            var result = await graphClient.Users.GetAsync();
            return result.Value.Select(u => new DirectoryUser { Id = u.Id, Email = u.Mail, Name = u.DisplayName }).ToList();
        }
        public async Task<DirectoryUser> GetUserIdByEmail(string email)
        {
            try
            {
                var result = await graphClient.Users.GetAsync((requestConfiguration) =>
                {
                    requestConfiguration.QueryParameters.Count = true;
                    requestConfiguration.QueryParameters.Search = $"\"mail:{email}\"";
                    requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "mail" };
                    requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");
                });
                return new DirectoryUser { Id = result.Value[0].Id, Email = result.Value[0].Mail, Name = result.Value[0].DisplayName };
            }
            catch(ODataError odataError)
            {
                _logger.LogError("{0}: Could not retrieve user with email: {1}", odataError.Message, email);
                throw odataError;
            }  
        }

        public async Task<List<Directory>> GetUserDirectoriesByEmail(string email)
        {
            List<Directory> results = new List<Directory>();
            try
            {
                // Get the UserId from the User's Email
                var result = await graphClient.Users.GetAsync((requestConfiguration) =>
                {
                    requestConfiguration.QueryParameters.Count = true;
                    requestConfiguration.QueryParameters.Search = $"\"mail:{email}\"";
                    requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "mail" };
                    requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");
                });
                var userId = result.Value[0].Id;
                // Get the Groups to which the member belongs
                var groups = await graphClient.Users[userId].MemberOf.GraphGroup.GetAsync((requestConfiguration) =>
                {
                    requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName" };
                    requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");
                });
                foreach (var group in groups.Value)
                {
                    var drives = await graphClient.Groups[group.Id].Drives.GetAsync((requestConfiguration) =>
                    {
                        requestConfiguration.QueryParameters.Select = new string[] { "id", "name" };
                    });
                    foreach (var drive in drives.Value)
                    {
                        var item = await graphClient.Drives[drive.Id].Root.GetAsync((requestConfiguration) =>
                        {
                            requestConfiguration.QueryParameters.Select = new string[] { "id" };
                        });

                        results.Add(new Directory { DriveId = drive.Id, DriveName = group.DisplayName, ListId = item.Id, ListName = drive.Name});

                        
                    }
                }
                if(results.Count == 0) // if no drives are found, add the default drive from settings.
                {
                    foreach (var drive in _azureAdSettings.Drives)
                    {
                        foreach(var list in drive.Lists)
                        {
                            results.Add(new Directory { DriveId = drive.Id, DriveName = drive.Name, ListId = list.Id, ListName = "Default"});
                        }
                    }
                }
                return results;
            }
            catch (ODataError odataError)
            {
                _logger.LogError("{0}: Could not retrieve user with email: {1}", odataError.Message, email);
                throw odataError;
            }
        }
    }
}
