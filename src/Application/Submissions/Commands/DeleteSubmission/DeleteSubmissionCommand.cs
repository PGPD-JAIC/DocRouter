using DocRouter.Application.Common.Models;
using MediatR;

namespace DocRouter.Application.Submissions.Commands.DeleteSubmission
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that handles a request to delete a submission.
    /// </summary>
    public class DeleteSubmissionCommand : IRequest<Result>
    {
        /// <summary>
        /// The id of the submission to be deleted.
        /// </summary>
        public int Id { get; set; }
    }
}
