using System;

namespace DocRouter.Application.Common.Exceptions
{
    /// <summary>
    /// Implementation of <see cref="Exception"/> used in the application layer to handle bad requests.
    /// </summary>
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the exception message.</param>
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
