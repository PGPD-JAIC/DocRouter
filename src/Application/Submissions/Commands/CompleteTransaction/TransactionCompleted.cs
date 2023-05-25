using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Application.Submissions.Commands.ApproveTransaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DocRouter.Application.Submissions.Commands.CompleteTransaction
{
    /// <summary>
    /// Implementation of <see cref="INotification"/> that is triggered after a transaction is completed.
    /// </summary>
    public class TransactionCompleted : INotification
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
        public class TransactionCompletedHandler : INotificationHandler<TransactionCompleted>
        {
            private readonly INotificationService _notificationService;
            /// <summary>
            /// Creates a new instance of the handler.
            /// </summary>
            /// <param name="notificationService">An implementation of <see cref="INotificationService"/></param>
            public TransactionCompletedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }
            /// <summary>
            /// Handles the notification.
            /// </summary>
            /// <param name="notification">A <see cref="TransactionCompleted"/> object.</param>
            /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
            /// <returns></returns>
            public async Task Handle(TransactionCompleted notification, CancellationToken cancellationToken)
            {
                List<MessageDto> toSend = new List<MessageDto>
                {
                    new MessageDto
                    {
                        //TODO: Remove manual address used in testing
                        // To = notification.SubmittedBy,
                        To = "jcsmith1@co.pg.md.us",
                        From = "DocRouter",
                        Subject = $"{notification.SubmissionTitle} is Complete.",
                        Body = $"<p>The submission you forwarded was completed. You can view the documents <a href=\"{notification.SubmissionUri}\">here</a></p>",
                        SubmissionId = notification.SubmissionId
                    }
                };
                await Task.WhenAll(toSend.Select(x => _notificationService.SendAsync(x)));
            }
        }
    }
}
