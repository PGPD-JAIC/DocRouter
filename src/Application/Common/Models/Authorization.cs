using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Models
{
    /// <summary>
    /// Assembly that contains Authorization handlers.
    /// </summary>
    public class Authorization
    {
        //public class CanUpdateActivityAuthorizer : IAuthorizer<UpdateActivityCommand>
        //{
        //    private readonly IMediator _mediator;
        //    /// <summary>
        //    /// Creates a new instance of the authorizer.
        //    /// </summary>
        //    /// <param name="mediator">An implementation of <see cref="IMediator"/></param>
        //    public CanUpdateActivityAuthorizer(IMediator mediator)
        //    {
        //        _mediator = mediator;
        //    }
        //    /// <summary>
        //    /// Performs the auth check.
        //    /// </summary>
        //    /// <param name="instance">The instance of the request command.</param>
        //    /// <param name="cancellationToken">A cancellationToken.</param>
        //    public async Task<AuthorizationResult> AuthorizeAsync(UpdateActivityCommand instance, CancellationToken cancellationToken = default)
        //    {
        //        bool result = await _mediator.Send(new GetCanEditActivityQuery());
        //        if (result == true)
        //        {
        //            return AuthorizationResult.Succeed();
        //        }
        //        return AuthorizationResult.Fail("You are not authorized to update this Activity.");
        //    }
        //}
    }
}
