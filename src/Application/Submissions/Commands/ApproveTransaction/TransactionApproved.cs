using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.ApproveTransaction
{
    /// <summary>
    /// Implementation of <see cref="INotification"/> that is triggered after a transaction is approved.
    /// </summary>
    public class TransactionApproved : INotification
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
        /// The name of the person to whom the transaction has been submitted.
        /// </summary>
        public string SubmittedTo { get; set; }
        /// <summary>
        /// The title of the submission.
        /// </summary>
        public string SubmissionTitle { get; set; }
        /// <summary>
        /// Implementation of <see cref="INotificationHandler{TNotification}"/> that handles the notification.
        /// </summary>
        public class TransactionApprovedHandler : INotificationHandler<TransactionApproved>
        {
            private readonly INotificationService _notificationService;
            /// <summary>
            /// Creates a new instance of the handler.
            /// </summary>
            /// <param name="notificationService">An implementation of <see cref="INotificationService"/></param>
            public TransactionApprovedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }
            /// <summary>
            /// Handles the notification.
            /// </summary>
            /// <param name="notification">A <see cref="TransactionApproved"/> object.</param>
            /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
            /// <returns></returns>
            public async Task Handle(TransactionApproved notification, CancellationToken cancellationToken)
            {
                List<MessageDto> toSend = new List<MessageDto>
                {
                    new MessageDto
                    {
                        //TODO: Remove manual address used in testing
                        // To = notification.SubmittedTo,
                        To = "jcsmith1@co.pg.md.us",
                        From = "DocRouter",
                        Subject = $"{notification.SubmissionTitle} requires your approval.",
                        Body = $"<p>A submission has been forwarded to you for review. You can view the documents <a href=\"{notification.SubmissionUri}\">here</a></p>",
                        SubmissionId = notification.SubmissionId
                    },
                    new MessageDto
                    {
                        //TODO: Remove manual address used in testing
                        // To = notification.SubmittedBy,
                        To = "jcsmith1@co.pg.md.us",
                        From = "DocRouter",
                        Subject = $"{notification.SubmissionTitle} was Approved.",
                        Body = $"<p>The submission you forwarded was approved. You can view the documents <a href=\"{notification.SubmissionUri}\">here</a></p>",
                        SubmissionId = notification.SubmissionId
                    }
                };
                await Task.WhenAll(toSend.Select(x => _notificationService.SendAsync(x)));
            }
        }
    }
}
