using DocRouter.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Behaviors
{
    /// <summary>
    /// Logs incoming requests.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        /// <summary>
        /// Creates a new instance of the logger
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="currentUserService"></param>
        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }
        /// <summary>
        /// Processes the Request into logging
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            _logger.LogInformation($"DocRouter Request: {name} {_currentUserService.UserId} {request}");
            return Task.CompletedTask;
        }
    }
}
