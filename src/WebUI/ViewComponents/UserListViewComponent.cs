using DocRouter.Application.Users.Queries.GetADUserList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DocRouter.WebUI.ViewComponents
{
    public class UserListViewComponent : ViewComponent
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public UserListViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(string selectedId, string propertyName)
        {
            var result = await Mediator.Send(new GetADUserListQuery());
            UserListVm vm = new UserListVm
            {
                Users = new SelectList(result.Users, "Id", "Name", selectedId),
                PropertyName = propertyName
            };
            return View(vm);
        }
    }
    public class UserListVm
    {
        public SelectList Users { get; set; } 
        public string PropertyName { get; set; }
    }
}
