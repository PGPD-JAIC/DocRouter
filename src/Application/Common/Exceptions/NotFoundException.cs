using System;

namespace DocRouter.Application.Common.Exceptions
{
    /// <summary>
    /// An implementation of <see cref="Exception"></see> used in the DocRouter.Application namespace
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the exception
        /// </summary>
        /// <param name="name">The name of the Entity</param>
        /// <param name="key">The key of the Entity that was not found.</param>
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
