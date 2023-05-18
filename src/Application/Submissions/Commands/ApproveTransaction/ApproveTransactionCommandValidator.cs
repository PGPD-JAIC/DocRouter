using DocRouter.Application.Submissions.Queries.GetApproveTransactionDetail;
using FluentValidation;

namespace DocRouter.Application.Submissions.Commands.ApproveTransaction
{
    public class ApproveTransactionCommandValidator : AbstractValidator<ApproveTransactionCommand>
    {
        public ApproveTransactionCommandValidator()
        {
            RuleFor(x => x.SubmissionId)
                .NotEmpty()
                    .WithMessage("Submission Id is required.");
            RuleFor(x => x.NewComments)
                .NotEmpty()
                    .WithMessage("Comments are required.")
                .MaximumLength(5000)
                    .WithMessage("Comments must be 5000 characters or fewer.");
            RuleFor(x => x.Recepient)
                .NotEmpty()
                    .WithMessage("Please select a recepient.");
        }
    }
}
