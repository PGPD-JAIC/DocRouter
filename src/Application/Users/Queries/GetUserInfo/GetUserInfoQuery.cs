using MediatR;

namespace DocRouter.Application.Users.Queries.GetUserInfo
{
    /// <summary>
    /// Implementation of <see cref="IRequest{UserInfoVm}"></see> that returns details of the current user.
    /// </summary>
    public class GetUserInfoQuery : IRequest<UserInfoVm>
    {
    }
}
