using DocRouter.Application.Users.Queries.GetUserDirectories;
using DocRouter.Infrastructure.Azure;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocRouter.WebUI.ViewComponents
{
    public class UserDriveListViewComponent: ViewComponent
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public UserDriveListViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(string selectedDriveId, string drivePropertyName)
        {
            var result = await Mediator.Send(new GetUserDirectoryListQuery());
            var driveList = new List<SelectListItem>();
            foreach (var drive in result.Directories)
            {
                bool selected = drive.DriveId + drive.ListId == selectedDriveId;
                driveList.Add(new SelectListItem($"{drive.DriveName}: {drive.ListName}", $"{drive.DriveId},{drive.ListId}", selected));
            }
            UserDriveListViewComponentVm vm = new UserDriveListViewComponentVm
            {
                Drives = driveList,
                DrivePropertyName = drivePropertyName
            };
            return View(vm);
        }
    }
    public class UserDriveListViewComponentVm
    {
        public List<SelectListItem> Drives { get; set; }
        public string DrivePropertyName { get; set; }
    }
}
