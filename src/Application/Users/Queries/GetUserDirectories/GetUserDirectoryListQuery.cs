using MediatR;

namespace DocRouter.Application.Users.Queries.GetUserDirectories
{
    /// <summary>
    /// Implementation of <see cref="IRequest{UserDirectoryListVm}"></see> that retrieves a list of directories available to the user.
    /// </summary>
    public class GetUserDirectoryListQuery : IRequest<UserDirectoryListVm>
    {
    }
}
