using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetCompleteTransactionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that retrieves details for a transaction completion operation.
    /// </summary>
    public class GetCompleteTransactionDetailQuery : IRequest<CompleteTransactionCommand>
    {
        /// <summary>
        /// The Id of the Transaction.
        /// </summary>
        public int TransactionId { get; set; }
    }
}
