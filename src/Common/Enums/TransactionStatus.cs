namespace DocRouter.Common.Enums
{
    /// <summary>
    /// Enumeration that represents a transaction status.
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// The transaction is pending review.
        /// </summary>
        Pending,
        /// <summary>
        /// The transaction was approved.
        /// </summary>
        Approved,
        /// <summary>
        /// The transaction was rejected.
        /// </summary>
        Rejected, 
        /// <summary>
        /// The transaction was completed.
        /// </summary>
        Complete,
        /// <summary>
        /// The transaction was recalled by the submittor.
        /// </summary>
        Recalled
    }
}
