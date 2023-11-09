using DocRouter.Application.Submissions.Queries.GetEditSubmissionDetail;
using FluentValidation;

namespace DocRouter.Application.Submissions.Commands.EditSubmission
{
    /// <summary>
    /// Implementation of <see cref="AbstractValidator{T}"/> used to validate values in a <see cref="EditSubmissionCommand"/>
    /// </summary>
    public class EditSubmissionCommandValidator : AbstractValidator<EditSubmissionCommand>
    {
        /// <summary>
        /// Creates a new instance of the validator.
        /// </summary>
        public EditSubmissionCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage("A title or name is required.")
                .MaximumLength(100)
                    .WithMessage("Maximum length of 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage("A description is required.")
                .MaximumLength(1000)
                    .WithMessage("Maximum length of 1000 characters.");
        }
    }
}
