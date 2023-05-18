using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocRouter.Application.Common.Exceptions
{
    /// <summary>
    /// Implementation of <see cref="Exception"></see> used in the application namespace
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Creates a new Instance of the Exception
        /// </summary>
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="failures"></param>
        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
        /// <summary>
        /// Contains the list of failed validation key/values
        /// </summary>
        public IDictionary<string, string[]> Failures { get; }
    }
}
