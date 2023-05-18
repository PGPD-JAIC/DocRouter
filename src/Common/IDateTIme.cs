using System;

namespace DocRouter.Common
{
    /// <summary>
    /// Interface that defines a DateTime provider
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Returns the current Date/Time
        /// </summary>
        DateTime Now { get; }
    }
}
