namespace DocRouter.Application.Common.Models
{
    /// <summary>
    /// Data transfer class used to relay details for a Message.
    /// </summary>
    public class MessageDto
    {
        /// <summary>
        /// The message sender.
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// The message recipient.
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// The message subject line.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The message body.
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// The Id of the submission associated with the email.
        /// </summary>
        public int SubmissionId { get; set; }
    }
}
