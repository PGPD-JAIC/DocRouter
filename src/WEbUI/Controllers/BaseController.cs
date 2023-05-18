using DocRouter.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DocRouter.WebUI.Controllers
{
    public abstract class BaseController : Controller
    {
        private IMediator _mediator;
        private IDateTime _dateTime;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IDateTime DateTime => _dateTime ??= HttpContext.RequestServices.GetService<IDateTime>();
    }
}