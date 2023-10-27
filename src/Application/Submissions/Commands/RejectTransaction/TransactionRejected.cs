using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.RejectTransaction
{
    /// <summary>
    /// Implementation of <see cref="INotification"/> that is triggered after a transaction is rejected.
    /// </summary>
    public class TransactionRejected : INotification
    {
        /// <summary>
        /// The integer ID of the submission.
        /// </summary>
        public int SubmissionId { get; set; }
        /// <summary>
        /// The Uri of the submision folder.
        /// </summary>
        public string SubmissionUri { get; set; }
        /// <summary>
        /// The Name of the person who submitted the transaction.
        /// </summary>
        public string SubmittedBy { get; set; }
        /// <summary>
        /// The name of the person rejecting transaction.
        /// </summary>
        public string RejectedBy { get; set; }
        /// <summary>
        /// The title of the subission.
        /// </summary>
        public string SubmissionTitle { get; set; }
        /// <summary>
        /// Implementation of <see cref="INotificationHandler{TransactionRejected}"/> that handles the notification.
        /// </summary>
        public class TransactionRejectedHandler : INotificationHandler<TransactionRejected>
        {
            private readonly INotificationService _notificationService;
            /// <summary>
            /// Creates a new instance of the handler.
            /// </summary>
            /// <param name="notificationService">An implementation of <see cref="INotificationService"/></param>
            public TransactionRejectedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }
            /// <summary>
            /// Handles the notifications.
            /// </summary>
            /// <param name="notification">A <see cref="TransactionRejected"/> object.</param>
            /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
            /// <returns></returns>
            public async Task Handle(TransactionRejected notification, CancellationToken cancellationToken)
            {
                List<MessageDto> toSend = new List<MessageDto>
                {
                    new MessageDto
                    {
                        //TODO: Remove manual address used in testing
                        // To = notification.SubmittedTo,
                        To = "jcsmith1@co.pg.md.us",
                        From = "DocRouter",
                        Subject = $"{notification.SubmissionTitle} was Rejected.",
                        Body = $"<p>A submission was rejected to you for review. You can view the documents <a href=\"{notification.SubmissionUri}\">here</a></p>",
                        SubmissionId = notification.SubmissionId
                    }
                };
                await Task.WhenAll(toSend.Select(x => _notificationService.SendAsync(x)));
            }
        }
    }
}
