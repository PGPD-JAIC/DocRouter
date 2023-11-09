using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.CreateSubmission
{
    /// <summary>
    /// Implementation of <see cref="INotification"/> that handles notifications following the creation of a new submission.
    /// </summary>
    public class SubmissionCreated : INotification
    {
        /// <summary>
        /// The Id of the Submission.
        /// </summary>
        public int SubmissionId { get; set; }
        /// <summary>
        /// The URI of the submission.
        /// </summary>
        public string SubmissionUri { get; set; }
        /// <summary>
        /// The Email address of the person creating the submission.
        /// </summary>
        public string SubmittedBy { get; set; }
        /// <summary>
        /// The email address of the person to whom the submission has been submitted.
        /// </summary>
        public string SubmittedTo { get; set; }
        /// <summary>
        /// The title of the submission.
        /// </summary>
        public string SubmissionTitle { get; set; }
        /// <summary>
        /// Implementation of <see cref="INotificationHandler{SubmissionCreated}"/> that handles the notification.
        /// </summary>
        public class SubmissionCreatedHandler : INotificationHandler<SubmissionCreated>
        {
            private readonly INotificationService _notificationService;
            /// <summary>
            /// Creates a new instance of the handler.
            /// </summary>
            /// <param name="notificationService"></param>
            public SubmissionCreatedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }
            /// <summary>
            /// Handles the request.
            /// </summary>
            /// <param name="notification"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task Handle(SubmissionCreated notification, CancellationToken cancellationToken)
            {

                var message = new MessageDto
                {
                    //TODO: Remove manual address used in testing
                    // To = notification.SubmittedBy,
                    To = "jcsmith1@co.pg.md.us",
                    From = "DocRouter",
                    Subject = notification.SubmissionTitle,
                    Body = $"<p>A submission has been forwarded to for your review. You can view the documents <a href=\"{notification.SubmissionUri}\">here</a></p>",
                    SubmissionId = notification.SubmissionId
                };

                await _notificationService.SendAsync(message);
            }
        }
    }
}
