using DocRouter.Application.Common.Exceptions;
using DocRouter.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Behaviors
{
    /// <summary>
    /// Behavior that performs authorization on requests.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class RequestAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IAuthorizer<TRequest>> _authorizers;
        /// <summary>
        /// Creates a new instance of the Behavior.
        /// </summary>
        /// <param name="authorizers"></param>
        public RequestAuthorizationBehavior(IEnumerable<IAuthorizer<TRequest>> authorizers)
        {
            _authorizers = authorizers;
        }
        /// <summary>
        /// Handles the authorization request.
        /// </summary>
        /// <param name="request">An implementation of <see cref="IRequest"/></param>
        /// <param name="cancellationToken">A cancellationToken</param>
        /// <param name="next">A <see cref="RequestHandlerDelegate{TResponse}"/></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            foreach (var authorizer in _authorizers)
            {
                var result = await authorizer.AuthorizeAsync(request, cancellationToken);
                if (!result.IsAuthorized)
                    throw new UnauthorizedException(result.FailureMessage);
            }

            return await next();
        }
    }
}
