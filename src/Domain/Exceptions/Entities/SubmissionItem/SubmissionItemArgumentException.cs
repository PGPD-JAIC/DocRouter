using System;

namespace DocRouter.Domain.Exceptions.Entities.SubmissionItem
{
    /// <summary>
    /// Implementation of <see cref="ArgumentException"/> that is used in the <see cref="SubmissionItem"/> entity class.
    /// </summary>
    public class SubmissionItemArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new Instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public SubmissionItemArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }

    }
}
