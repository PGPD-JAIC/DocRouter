using System;

namespace DocRouter.Application.Common.Exceptions
{
    /// <summary>
    /// Implementation of <see cref="Exception"></see> used in the Application namespace when a Authorization operation fails.
    /// </summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Creates a new instance of the Exception
        /// </summary>
        /// <param name="message">A string containing the exception message.</param>
        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
