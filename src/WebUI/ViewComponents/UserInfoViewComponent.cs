using DocRouter.Application.Users.Queries.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DocRouter.WebUI.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public UserInfoViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await Mediator.Send(new GetUserInfoQuery()));
        }
    }
}
