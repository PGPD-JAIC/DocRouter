using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.CreateSubmission
{
    public class SubmissionCreated : INotification
    {
        public int SubmissionId { get; set; }
        public string SubmissionUri { get; set; }
        public string SubmittedBy { get; set; }
        public string SubmittedTo { get; set; }
        public string SubmissionTitle { get; set; }
        public class SubmissionCreatedHandler : INotificationHandler<SubmissionCreated>
        {
            private readonly INotificationService _notificationService;
            
            public SubmissionCreatedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }
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
