using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.ApproveTransaction
{
    public class TransactionApproved : INotification
    {
        public int SubmissionId { get; set; }
        public string SubmissionUri { get; set; }
        public string SubmittedBy { get; set; }
        public string SubmittedTo { get; set; }
        public string SubmissionTitle { get; set; }

        public class TransactionApprovedHandler : INotificationHandler<TransactionApproved>
        {
            private readonly INotificationService _notificationService;
            public TransactionApprovedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }
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
