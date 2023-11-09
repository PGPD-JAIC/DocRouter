using FluentValidation;

namespace DocRouter.Application.Submissions.Queries.GetUserSubmissions
{
    /// <summary>
    /// Implementation of <see cref="AbstractValidator{T}"/> used to validate values in a <see cref="GetUserSubmissionsQuery"/>
    /// </summary>
    public class GetUserSubmissionsQueryValidator : AbstractValidator<GetUserSubmissionsQuery>
    {
        /// <summary>
        /// Creates a new instance of the validator.
        /// </summary>
        public GetUserSubmissionsQueryValidator()
        {
            RuleFor(x => x.TitleSearch)
                .MaximumLength(50)
                .WithMessage("Submission Title search limited to 50 characters.");
        }
    }
}
