using System;

namespace DocRouter.Domain.Exceptions
{
    /// <summary>
    /// Implementation of <see cref="ArgumentException"/> used in the Base Entity class.
    /// </summary>
    public class BaseEntityArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new Instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public BaseEntityArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
