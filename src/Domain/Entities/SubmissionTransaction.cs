using DocRouter.Common.Enums;
using DocRouter.Domain.Common;
using DocRouter.Domain.Exceptions.Entities.SubmissionTransaction;
using System;

namespace DocRouter.Domain.Entities
{
    public class SubmissionTransaction : BaseEntity
    {
        /// <summary>
        /// Parameterless constructor required by EF.
        /// </summary>
        private SubmissionTransaction() { }
        public SubmissionTransaction(DateTime transactionDate, DateTime currentDate, TransactionStatus status, string routedTo, string comments)
        {
            UpdateTransactionDate(transactionDate, currentDate);
            UpdateTransactionStatus(status);
            UpdateRoutedTo(routedTo);
            UpdateComments(comments);
        }
        private int _submissionId;
        /// <summary>
        /// Returns the Id of the submission associated with the Transaction.
        /// </summary>
        public int SubmissionId => _submissionId;
        /// <summary>
        /// Navigation property to the <see cref="Submission"/> associated with the transaction.
        /// </summary>
        public virtual Submission Submission { get; private set; }
        private DateTime _transactionDate;
        /// <summary>
        /// The date/time of the transaction.
        /// </summary>
        public DateTime TransactionDate => _transactionDate;
        private TransactionStatus _status;
        /// <summary>
        /// The status of the transaction.
        /// </summary>
        public TransactionStatus Status => _status;
        private string _routedTo;
        /// <summary>
        /// The email address of the person to whom the submission is routed for this transaction.
        /// </summary>
        public string RoutedTo => _routedTo;
        private string _comments;
        /// <summary>
        /// The user comments associated with the transaction.
        /// </summary>
        public string Comments => _comments;
        /// <summary>
        /// Updates the date of the transaction.
        /// </summary>
        /// <param name="newTransactionDate">A <see cref="DateTime"/> containing the transaction date.</param>
        /// <param name="currentDate">A <see cref="DateTime"/> containing the current date/time.</param>
        /// <exception cref="SubmissionTransactionArgumentException">Thrown when the newTranaction date is in the past.</exception>
        public void UpdateTransactionDate(DateTime newTransactionDate, DateTime currentDate)
        {
            if (newTransactionDate.Date < currentDate.Date)
            {
                throw new SubmissionTransactionArgumentException("Parameter cannot be a date older than the current date.", nameof(newTransactionDate));
            }
            _transactionDate = newTransactionDate;
        }
        /// <summary>
        /// Updates the Status of the transaction.
        /// </summary>
        /// <param name="newStatus">A <see cref="TransactionStatus"/> value.</param>
        public void UpdateTransactionStatus(TransactionStatus newStatus) 
        {
            _status = newStatus;
        }
        /// <summary>
        /// Updates the recipient's email address.
        /// </summary>
        /// <param name="newRoutedTo"></param>
        public void UpdateRoutedTo(string newRoutedTo)
        {
            if (string.IsNullOrWhiteSpace(newRoutedTo))
            {
                throw new SubmissionTransactionArgumentException("Parameter cannot be null or whitespace.", nameof(newRoutedTo));
            }
            else if (newRoutedTo.Length > 100)
            {
                throw new SubmissionTransactionArgumentException("Parameter must be 100 characters or fewer.", nameof(newRoutedTo));
            }
            _routedTo = newRoutedTo;
        }
        /// <summary>
        /// Updates the comments associated with the transaction.
        /// </summary>
        /// <param name="newComments">A string containing the new comments.</param>
        /// <exception cref="SubmissionTransactionArgumentException">Thrown when the provided parameter is more than 5000 characters.</exception>
        public void UpdateComments(string newComments)
        {
            if (newComments.Length > 5000)
            {
                throw new SubmissionTransactionArgumentException("Parameter must be 5000 characters or fewer.", nameof(newComments));
            }
            _comments = newComments;
        }

    }
}
