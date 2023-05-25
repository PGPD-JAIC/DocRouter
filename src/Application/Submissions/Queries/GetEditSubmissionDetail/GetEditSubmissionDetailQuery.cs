using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetEditSubmissionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that retrieves submission details for an edit operation.
    /// </summary>
    public class GetEditSubmissionDetailQuery : IRequest<EditSubmissionCommand>
    {
        /// <summary>
        /// The Id of the Submission to edit.
        /// </summary>
        public int Id { get; set; }
    }
}
