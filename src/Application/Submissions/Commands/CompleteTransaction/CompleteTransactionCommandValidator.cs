using DocRouter.Application.Submissions.Queries.GetCompleteTransactionDetail;
using FluentValidation;

namespace DocRouter.Application.Submissions.Commands.CompleteTransaction
{
    /// <summary>
    /// Implementation of <see cref="AbstractValidator{T}"/> that validates a <see cref="CompleteTransactionCommand"/>
    /// </summary>
    public class CompleteTransactionCommandValidator : AbstractValidator<CompleteTransactionCommand>
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public CompleteTransactionCommandValidator()
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
