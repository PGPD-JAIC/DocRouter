using DocRouter.Common.Enums;
using DocRouter.Domain.Common;
using DocRouter.Domain.Exceptions.Entities.SubmissionTransaction;
using System;

namespace DocRouter.Domain.Entities
{
    /// <summary>
    /// Entity class that represents a transaction associated with a <see cref="Entities.Submission"/>
    /// </summary>
    public class SubmissionTransaction : BaseEntity
    {
        /// <summary>
        /// Parameterless constructor required by EF.
        /// </summary>
        private SubmissionTransaction() { }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="transactionDate">The date/time that the transaction was created.</param>
        /// <param name="currentDate">The current date/time.</param>
        /// <param name="routedTo">The username of the user to whom the submission is routed in the transaction.</param>
        /// <param name="routedFrom">The username of the person submitting the transaction.</param>
        /// <param name="comments">The comments associated with the transaction.</param>
        public SubmissionTransaction(
            DateTime transactionDate, 
            DateTime currentDate,
            string routedTo, 
            string routedFrom, 
            string comments)
        {
            UpdateSubmitDate(transactionDate, currentDate);
            UpdateTransactionStatus(TransactionStatus.Pending);
            UpdateRoutedTo(routedTo);
            UpdateRoutedFrom(routedFrom);
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
        private DateTime _submitDate;
        /// <summary>
        /// The date/time of the transaction.
        /// </summary>
        public DateTime SubmitDate => _submitDate;
        private TransactionStatus _status;
        /// <summary>
        /// The status of the transaction.
        /// </summary>
        public TransactionStatus Status => _status;
        private DateTime? _responseDate;
        /// <summary>
        /// The date of response.
        /// </summary>
        public DateTime? ResponseDate => _responseDate;
        private string _routedTo;
        /// <summary>
        /// The email address of the person to whom the submission is routed for this transaction.
        /// </summary>
        public string RoutedTo => _routedTo;
        private string _routedFrom;
        /// <summary>
        /// The email address of the person who submitted the transaction.
        /// </summary>
        public string RoutedFrom => _routedFrom;
        private string _comments;
        /// <summary>
        /// The user comments associated with the transaction.
        /// </summary>
        public string Comments => _comments;
        /// <summary>
        /// Updates the date of submission.
        /// </summary>
        /// <param name="newTransactionDate">A <see cref="DateTime"/> containing the transaction date.</param>
        /// <param name="currentDate">A <see cref="DateTime"/> containing the current date/time.</param>
        /// <exception cref="SubmissionTransactionArgumentException">Thrown when the newTranaction date is in the past.</exception>
        private void UpdateSubmitDate(DateTime newTransactionDate, DateTime currentDate)
        {
            if (newTransactionDate.Date < currentDate.Date)
            {
                throw new SubmissionTransactionArgumentException("Parameter cannot be a date older than the current date.", nameof(newTransactionDate));
            }
            _submitDate = newTransactionDate;
        }
        /// <summary>
        /// Updates the date of response.
        /// </summary>
        /// <param name="newResponseDate">A <see cref="DateTime"/> containing the date that the response was entered.</param>
        /// <exception cref="SubmissionTransactionArgumentException"></exception>
        private void UpdateResponseDate(DateTime newResponseDate)
        {
            if (newResponseDate < _submitDate)
            {
                throw new SubmissionTransactionArgumentException("Cannot set response date to before submit date", nameof(newResponseDate));
            }
            _responseDate = newResponseDate;
        }
        /// <summary>
        /// Updates the Status of the transaction.
        /// </summary>
        /// <param name="newStatus">A <see cref="TransactionStatus"/> value.</param>
        private void UpdateTransactionStatus(TransactionStatus newStatus) 
        {
            _status = newStatus;
        }
        /// <summary>
        /// Updates the recipient's email address.
        /// </summary>
        /// <param name="newRoutedTo">A string containing the email address of the person to whom the transaction is being submitted.</param>
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
        /// Updates the sender's email address.
        /// </summary>
        /// <param name="newRoutedFrom">A string containing the email address of the person submitting the transaction.</param>
        public void UpdateRoutedFrom(string newRoutedFrom)
        {
            if (string.IsNullOrWhiteSpace(newRoutedFrom))
            {
                throw new SubmissionTransactionArgumentException("Parameter cannot be null or whitespace.", nameof(newRoutedFrom));
            }
            else if (newRoutedFrom.Length > 100)
            {
                throw new SubmissionTransactionArgumentException("Parameter must be 100 characters or fewer.", nameof(newRoutedFrom));
            }
            _routedFrom = newRoutedFrom;
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
        /// <summary>
        /// Approves the transaction.
        /// </summary>
        /// <param name="statusDate">A <see cref="DateTime"/> containing the date/time that the transaction was approved.</param>
        public void Approve(DateTime statusDate)
        {
            if (statusDate < _submitDate)
            {
                throw new SubmissionTransactionArgumentException("Parameter must be after the Transaction submitted date.", nameof(statusDate));
            }
            UpdateResponseDate(statusDate);
            UpdateTransactionStatus(TransactionStatus.Approved);
        }
        /// <summary>
        /// Completes the transaction.
        /// </summary>
        /// <param name="statusDate">A <see cref="DateTime"/> containing the date/time that the transaction was completed.</param>
        /// <exception cref="SubmissionTransactionArgumentException"></exception>
        public void Complete(DateTime statusDate)
        {
            if(statusDate < _created)
            {
                throw new SubmissionTransactionArgumentException("Parameter must be after the Transaction submitted date.", nameof(statusDate));
            }
            UpdateResponseDate(statusDate);
            UpdateTransactionStatus(TransactionStatus.Complete);
        }
        /// <summary>
        /// Recalls the transaction.
        /// </summary>
        /// <param name="statusDate">A <see cref="DateTime"/> containing the date/time that the transaction was recalled.</param>
        /// <exception cref="SubmissionTransactionArgumentException"></exception>
        public void Recall(DateTime statusDate)
        {
            if (statusDate < _created)
            {
                throw new SubmissionTransactionArgumentException("Parameter must be after the Transaction submitted date.", nameof(statusDate));
            }
            UpdateResponseDate(statusDate);
            UpdateTransactionStatus(TransactionStatus.Recalled);
        }
        /// <summary>
        /// Rejects the transaction.
        /// </summary>
        /// <param name="statusDate">A <see cref="DateTime"/> containing the date/time that the transaciton was rejected.</param>
        /// <exception cref="SubmissionTransactionArgumentException"></exception>
        public void Reject(DateTime statusDate)
        {
            if (statusDate < _created)
            {
                throw new SubmissionTransactionArgumentException("Parameter must be after the Transaction submitted date.", nameof(statusDate));
            }
            UpdateResponseDate(statusDate);
            UpdateTransactionStatus(TransactionStatus.Rejected);
        }

    }
}
