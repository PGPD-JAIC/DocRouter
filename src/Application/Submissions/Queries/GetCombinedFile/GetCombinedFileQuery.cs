using DocRouter.Application.Common.Models;
using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetCombinedFile
{
    /// <summary>
    /// Implementation of <see cref="IRequest{FileSubmissionDto}"/> that retrieves a single file containing all of the documents in a submission.
    /// </summary>
    public class GetCombinedFileQuery : IRequest<FileSubmissionDto>
    {
        /// <summary>
        /// The Id of the submission.
        /// </summary>
        public int Id { get; set; }
    }
}
