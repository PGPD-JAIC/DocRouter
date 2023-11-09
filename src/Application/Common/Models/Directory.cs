namespace DocRouter.Application.Common.Models
{
    /// <summary>
    /// Class that contains details of a directory.
    /// </summary>
    public class Directory
    {
        /// <summary>
        /// The Id of drive containing the Directory.
        /// </summary>
        public string DriveId { get; set; }
        /// <summary>
        /// The Name of the drive containing the directory.
        /// </summary>
        public string DriveName { get; set; }
        /// <summary>
        /// The Id of the list containing the directory.
        /// </summary>
        public string ListId { get; set; }
        /// <summary>
        /// The name of the list containing the directory.
        /// </summary>
        public string ListName { get; set; }
    }
}
