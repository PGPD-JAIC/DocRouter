using Microsoft.Graph.Models;
using System.Collections.Generic;

namespace DocRouter.Infrastructure.Azure
{
    /// <summary>
    /// Class that contains Azure Registration Details.
    /// </summary>
    public class AzureADSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string Instance { get; set; }
        public string GraphResource { get; set; }
        public string GraphResourceEndPoint { get; set; }
        public List<AzureADDrive> Drives { get;set; } = new List<AzureADDrive>();
    }
    /// <summary>
    /// Class that represents details for an Azure Drive
    /// </summary>
    public class AzureADDrive
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<AzureADDriveList> Lists { get; set; } = new List<AzureADDriveList>();
    }
    public class AzureADDriveList
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
