using DocRouter.Application.Submissions.Commands.CreateSubmission;
using DocRouter.Application.Submissions.Commands.DeleteSubmission;
using DocRouter.Application.Submissions.Queries.GetAllSubmissions;
using DocRouter.Application.Submissions.Queries.GetApproveTransactionDetail;
using DocRouter.Application.Submissions.Queries.GetCombinedFile;
using DocRouter.Application.Submissions.Queries.GetCompleteTransactionDetail;
using DocRouter.Application.Submissions.Queries.GetFile;
using DocRouter.Application.Submissions.Queries.GetRejectTransactionDetail;
using DocRouter.Application.Submissions.Queries.GetSubmissionDetail;
using DocRouter.Application.Submissions.Queries.GetSubmissionUsers;
using DocRouter.Application.Users.Queries.GetADUserList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DocRouter.WebUI.Controllers
{
    public class SubmissionController : BaseController
    {
        private readonly ILogger<SubmissionController> _logger;
        public SubmissionController(ILogger<SubmissionController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetAllSubmissionsQuery request)
        {
            return View(await Mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] GetSubmissionDetailQuery request, [FromQuery ]string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(await Mediator.Send(request));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateSubmissionCommand());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateSubmissionCommand form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var result = await Mediator.Send(form);
            TempData["Message"] = result.Message;
            return RedirectToAction("Success");
        }
        [HttpGet]
        public IActionResult Success()
        {
            ViewBag.Message = TempData["Message"].ToString();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Approve([FromQuery] GetApproveTransactionDetailQuery request, [FromQuery] string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var users = await Mediator.Send(new GetADUserListQuery());
            ViewBag.Users = users.Users;
            return View(await Mediator.Send(request));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve([FromForm] ApproveTransactionCommand command, [FromQuery] string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                var users = await Mediator.Send(new GetADUserListQuery());
                ViewBag.Users = users.Users;
                return View(command);
            }
            var result = await Mediator.Send(command);
            if (!string.IsNullOrEmpty(returnUrl)){
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Reject([FromQuery] GetRejectTransactionDetailQuery request, [FromQuery] string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var users = await Mediator.Send(new GetSubmissionUsersQuery() { TransactionId = request.TransactionId });
            ViewBag.Users = users;
            return View(await Mediator.Send(request));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject([FromForm] RejectTransactionCommand command, [FromQuery] string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                var users = await Mediator.Send(new GetSubmissionUsersQuery() { TransactionId = command.TransactionId });
                ViewBag.Users = users;
                return View(command);
            }
            var result = await Mediator.Send(command);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Complete([FromQuery] GetCompleteTransactionDetailQuery request, [FromQuery] string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(await Mediator.Send(request));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete([FromForm] CompleteTransactionCommand command, [FromQuery] string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(command);
            }
            var result = await Mediator.Send(command);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromQuery] GetSubmissionDetailQuery request, [FromQuery] string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(await Mediator.Send(request));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm] DeleteSubmissionCommand command, [FromQuery] string returnUrl)
        {
            await Mediator.Send(command);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> DownloadAsPdf([FromRoute] GetFileQuery request)
        {
            var vm = await Mediator.Send(request);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadCombinedPdf([FromRoute] GetCombinedFileQuery request)
        {
            var vm = await Mediator.Send(request);
            return File(vm.Content, vm.ContentType, vm.FileName);
        }
    }
}
