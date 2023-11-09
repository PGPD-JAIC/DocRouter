using DocRouter.Application.Common.Models;
using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetFile
{
    /// <summary>
    /// Implementation of <see cref="IRequest{FileSubmissionDto}"/> that retrieves a file.
    /// </summary>
    public class GetFileQuery : IRequest<FileSubmissionDto>
    {
        /// <summary>
        /// The Id of the file.
        /// </summary>
        public int Id { get; set; }
    }
}
