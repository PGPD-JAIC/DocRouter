using DocRouter.Application.Submissions.Queries.GetUserSubmissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DocRouter.WebUI.ViewComponents
{
    public class UserSubmissionListViewComponent : ViewComponent
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public UserSubmissionListViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync([FromQuery] GetUserSubmissionsQuery request)
        {
            return View(await Mediator.Send(request));
        }
    }
}
