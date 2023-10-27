using DocRouter.Application.Submissions.Commands.DeleteSubmission;
using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetDeleteSubmissionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that retrieves details of a <see cref="Domain.Entities.Submission"/> for a delete operation.
    /// </summary>
    public class GetDeleteSubmissionDetailQuery : IRequest<DeleteSubmissionCommand>
    {
        /// <summary>
        /// The Id of the Submission.
        /// </summary>
        public int SubmissionId { get; set; }
    }
}
