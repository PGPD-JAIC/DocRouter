namespace DocRouter.Infrastructure.Azure
{
    /// <summary>
    /// Class that contains Azure Registration Details.
    /// </summary>
    public class AzureAD
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string Instance { get; set; }
        public string GraphResource { get; set; }
        public string GraphResourceEndPoint { get; set; }
    }
}
