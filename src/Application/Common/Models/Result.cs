using System.Collections.Generic;
using System.Linq;

namespace DocRouter.Application.Common.Models
{
    /// <summary>
    /// Class that collects a request's results
    /// </summary>
    public class Result
    {
        public Result(bool succeeded, string message, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = errors.ToArray();
        }
        /// <summary>
        /// Indicates whether the request succeeded.
        /// </summary>
        public bool Succeeded { get; set; }
        /// <summary>
        /// A message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Contains any validation error messages.
        /// </summary>
        public string[] Errors { get; set; }
        /// <summary>
        /// Success result
        /// </summary>
        /// <returns></returns>
        public static Result Success()
        {
            return new Result(true, "", new string[] { });
        }
        public static Result Success(string message)
        {
            return new Result(true, message, new string[] { });
        }
        /// <summary>
        /// Failure result.
        /// </summary>
        /// <param name="errors">A string array containing any error messages.</param>
        /// <returns></returns>
        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, "", errors);
        }
        /// <summary>
        /// Failure result with message.
        /// </summary>
        /// <param name="message">A message.</param>
        /// <param name="errors">A string array containing any error messages.</param>
        /// <returns></returns>
        public static Result Failure(string message, IEnumerable<string> errors)
        {
            return new Result(false, message, errors);
        }
    }
}
