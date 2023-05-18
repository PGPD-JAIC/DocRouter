using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetApproveTransactionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that retrieves details for reviewing a transaction.
    /// </summary>
    public class GetApproveTransactionDetailQuery : IRequest<ApproveTransactionCommand>
    {
        /// <summary>
        /// The id of the requested transaction.
        /// </summary>
        public int TransactionId { get; set; }
    }
}
