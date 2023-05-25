using DocRouter.Application.Submissions.Commands.DeleteSubmission;
using DocRouter.Application.Submissions.Queries.GetADUserList;
using DocRouter.Application.Submissions.Queries.GetAllSubmissions;
using DocRouter.Application.Submissions.Queries.GetApproveTransactionDetail;
using DocRouter.Application.Submissions.Queries.GetCompleteTransactionDetail;
using DocRouter.Application.Submissions.Queries.GetDeleteSubmissionDetail;
using DocRouter.Application.Submissions.Queries.GetRejectTransactionDetail;
using DocRouter.Application.Submissions.Queries.GetSubmissionDetail;
using DocRouter.Application.Submissions.Queries.GetSubmissionUsers;
using DocRouter.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.DeviceManagement.Reports.GetCompliancePolicyNonComplianceSummaryReport;
using Microsoft.Graph.Education.Classes.Item.Assignments.Item.Submissions.Item.Return;
using System.Linq;
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
        public async Task<IActionResult> Create()
        {
            var users = await Mediator.Send(new GetADUserListQuery());
            SubmissionViewModel vm =
                new SubmissionViewModel()
                {
                    Users = users.Users.Select(x => new SelectListItem { Text = x.Name, Value = x.Email }).ToList()
                };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] SubmissionViewModel form)
        {
            if (!ModelState.IsValid)
            {
                var users = await Mediator.Send(new GetADUserListQuery());
                form.Users = users.Users.Select(x => new SelectListItem { Text = x.Name, Value = x.Email }).ToList();
                return View(form);
            }
            var result = await Mediator.Send(form.CreateSubmissionCommand());
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
        public async Task<IActionResult> Delete([FromQuery] GetDeleteSubmissionDetailQuery request, [FromQuery] string returnUrl)
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
    }
}
