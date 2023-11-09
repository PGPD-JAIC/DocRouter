using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Submissions.Queries.GetUserSubmissions;
using DocRouter.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DocRouter.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUserService _currentUserService;

        public HomeController(ILogger<HomeController> logger, ICurrentUserService currentUserService)
        {   
            _logger = logger;
            _currentUserService = currentUserService;
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetUserSubmissionsQuery request)
        {
            request.UserName = _currentUserService.UserId;
            return View(await Mediator.Send(request));
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult UserSubmissionList([FromQuery] GetUserSubmissionsQuery request)
        {
            return ViewComponent("PriorityList",
                request);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int code)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
