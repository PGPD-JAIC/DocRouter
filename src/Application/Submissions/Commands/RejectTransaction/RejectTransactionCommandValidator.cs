using DocRouter.Application.Submissions.Queries.GetRejectTransactionDetail;
using FluentValidation;

namespace DocRouter.Application.Submissions.Commands.RejectTransaction
{
    /// <summary>
    /// Implementation of <see cref="AbstractValidator{T}"/> that validates a <see cref="RejectTransactionCommand"/>
    /// </summary>
    public class RejectTransactionCommandValidator : AbstractValidator<RejectTransactionCommand>
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public RejectTransactionCommandValidator()
        {
            RuleFor(x => x.SubmissionId)
                .NotEmpty()
                    .WithMessage("Submission Id is required.");
            RuleFor(x => x.NewComments)
                .NotEmpty()
                    .WithMessage("Comments are required.")
                .MaximumLength(5000)
                    .WithMessage("Comments must be 5000 characters or fewer.");
        }

    }
}
