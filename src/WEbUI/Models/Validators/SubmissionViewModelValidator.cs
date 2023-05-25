using FluentValidation;

namespace DocRouter.WebUI.Models.Validators
{
    public class SubmissionViewModelValidator : AbstractValidator<SubmissionViewModel>
    {
        public SubmissionViewModelValidator()
        {
            RuleFor(x => x.Title)
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
