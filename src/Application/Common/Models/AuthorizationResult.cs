namespace DocRouter.Application.Common.Models
{
    /// <summary>
    /// Class containing the result of an authorization operation.
    /// </summary>
    public class AuthorizationResult
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public AuthorizationResult() { }
        /// <summary>
        /// Creates a new instance of the class from parameters.
        /// </summary>
        /// <param name="isAuthorized">Boolean indicating whether the authorization passed.</param>
        /// <param name="failureMessage">String containing any failure message.</param>
        private AuthorizationResult(bool isAuthorized, string failureMessage)
        {
            IsAuthorized = isAuthorized;
            FailureMessage = failureMessage;
        }
        /// <summary>
        /// Bool indicating whether the authorization operation passed.
        /// </summary>
        public bool IsAuthorized { get; }
        /// <summary>
        /// String containing the failure message
        /// </summary>
        public string FailureMessage { get; set; }
        /// <summary>
        /// Returns a failed instance with no message.
        /// </summary>
        /// <returns>A <see cref="AuthorizationResult"/></returns>
        public static AuthorizationResult Fail()
        {
            return new AuthorizationResult(false, null);
        }
        /// <summary>
        /// Returns a failed instance with a message.
        /// </summary>
        /// <param name="failureMessage">A string containing a failure message.</param>
        /// <returns>A <see cref="AuthorizationResult"/></returns>
        public static AuthorizationResult Fail(string failureMessage)
        {
            return new AuthorizationResult(false, failureMessage);
        }
        /// <summary>
        /// Returns a successful authorization result.
        /// </summary>
        /// <returns>A <see cref="AuthorizationResult"/></returns>
        public static AuthorizationResult Succeed()
        {
            return new AuthorizationResult(true, null);
        }
    }
}
