using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetRejectTransactionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that retrieves details of a <see cref="Submission"/>
    /// </summary>
    public class GetRejectTransactionDetailQuery : IRequest<SubmissionDto>
    {
        /// <summary>
        /// The Id of the desired submission.
        /// </summary>
        public int Id { get; set; }
    }
}
