using DocRouter.Application.Common.Models;
using MediatR;
using System.Collections.Generic;

namespace DocRouter.Application.Submissions.Commands.CreateSubmission
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that handles a request to create a submission.
    /// </summary>
    public class CreateSubmissionCommand : IRequest<Result>
    {
        /// <summary>
        /// The Email address of the person who will receive the submission.
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// The Name to assign to the submission.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The Description of the submission.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A string containing comments about the submission.
        /// </summary>
        public string Comments { get; set; } = "";
        /// <summary>
        /// A string containing the Id of the drive.
        /// </summary>
        public string DriveId { get; set; } = "b!j3sReZftLkuOgOZFHws5jx8M56sz-i9IkpjRrPkNncnrKJubNalgR6RgYT57FY72,013TPQOTV6Y2GOVW7725BZO354PWSELRRZ"; // TODO: remove hard-coded DriveId
        /// <summary>
        /// A string containing the Id of the list.
        /// </summary>
        public string ListId { get; set; }
        /// <summary>
        /// A list of files.
        /// </summary>
        public List<FileSubmissionDto> Files { get; set; }
    }
}
