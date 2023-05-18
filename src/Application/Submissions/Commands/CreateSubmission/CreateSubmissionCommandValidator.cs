using FluentValidation;

namespace DocRouter.Application.Submissions.Commands.CreateSubmission
{
    /// <summary>
    /// Implementation of <see cref="AbstractValidator{T}"/> used to validate values in a <see cref="CreateSubmissionCommand"/>
    /// </summary>
    public class CreateSubmissionCommandValidator : AbstractValidator<CreateSubmissionCommand>
    {
        public CreateSubmissionCommandValidator()
        {
            RuleFor(x => x.SubmissonName)
                .NotEmpty()
                    .WithMessage("A title or name is required.")
                .MaximumLength(100)
                    .WithMessage("Maximum length of 100 characters.");
            RuleFor(x => x.Files)
                .NotEmpty()
                .WithMessage("A submission requires at least 1 file.");
            RuleFor(x => x.Recipient)
                .NotEmpty()
                    .WithMessage("Please select a recipient.");
            RuleFor(x => x.Comments)
                .MaximumLength(5000)
                    .WithMessage("Maximum length of 5000 characters.");
        }
    }
}
