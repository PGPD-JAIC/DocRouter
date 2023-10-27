using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetRejectTransactionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that retrieves details of a <see cref="Domain.Entities.SubmissionTransaction"/>
    /// </summary>
    public class GetRejectTransactionDetailQuery : IRequest<RejectTransactionCommand>
    {
        /// <summary>
        /// The Id of the desired transaction.
        /// </summary>
        public int TransactionId { get; set; }
    }
}
