using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetSubmissionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that retrieves details of a <see cref="Domain.Entities.Submission"/>
    /// </summary>
    public class GetSubmissionDetailQuery : IRequest<SubmissionDto>
    {
        /// <summary>
        /// The Id of the desired submission.
        /// </summary>
        public int Id { get; set; }
    }
}
