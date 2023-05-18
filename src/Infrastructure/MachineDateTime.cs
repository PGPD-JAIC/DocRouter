using DocRouter.Common;
using System;

namespace DocRouter.Infrastructure
{
    /// <inheritdoc />
    public class MachineDateTime : IDateTime
    {
        /// <inheritdoc />
        public DateTime Now => DateTime.Now;
        /// <inheritdoc />
        public int CurrentYear => DateTime.Now.Year;
    }
}
