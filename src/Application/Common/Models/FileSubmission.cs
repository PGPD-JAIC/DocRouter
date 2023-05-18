using System.IO;

namespace DocRouter.Application.Common.Models
{
    /// <summary>
    /// Data transfer class that represents a file.
    /// </summary>
    public class FileSubmissionDto
    {
        /// <summary>
        /// The file's content in a stream.
        /// </summary>
        public Stream Content { get; set; }
        /// <summary>
        /// The content type of the file.
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// The name of the file.
        /// </summary>
        public string FileName { get; set; }
    }
}
