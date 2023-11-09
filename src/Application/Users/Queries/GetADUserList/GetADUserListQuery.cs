using MediatR;

namespace DocRouter.Application.Users.Queries.GetADUserList
{
    /// <summary>
    /// Implementation of <see cref="IRequest{ADUserListVm}"/> that returns a list of users.
    /// </summary>
    public class GetADUserListQuery : IRequest<ADUserListVm>
    {
    }
}
