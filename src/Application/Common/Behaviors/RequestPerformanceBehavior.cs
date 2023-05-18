using DocRouter.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Behaviors
{
    /// <summary>
    /// Implements <see cref="IPipelineBehavior{TRequest, TResponse}"></see> to log performance of Requests
    /// </summary>
    /// <typeparam name="TRequest">The Request</typeparam>
    /// <typeparam name="TResponse">The Response</typeparam>
    public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Creates a new Instance of the Class
        /// </summary>
        /// <param name="logger">An implementation of <see cref="ILogger"></see></param>
        /// <param name="currentUserService">An implementation of <see cref="ICurrentUserService"></see></param>
        public RequestPerformanceBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _currentUserService = currentUserService;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellation">A cancellation token</param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellation, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;
                _logger.LogWarning($"DocRouter Long Running Request: {name} ({_timer.ElapsedMilliseconds} milliseconds) {_currentUserService.UserId} {request}");
            }
            return response;
        }
    }
}