using FluentValidation;
using System.Collections.Generic;

namespace DocRouter.Application.Submissions.Queries.GetAllSubmissions
{
    /// <summary>
    /// Implementation of <see cref="AbstractValidator{T}"/> used to validate values in a <see cref="GetAllSubmissionsQuery"/>
    /// </summary>
    public class GetSubmissionListQueryValidator : AbstractValidator<GetAllSubmissionsQuery>
    {
        /// <summary>
        /// Creates a new instance of the validator.
        /// </summary>
        public GetSubmissionListQueryValidator()
        {
            RuleFor(x => x.SubmittedBySearch)
                .MaximumLength(50)
                .WithMessage("Submitted By search limited to 50 characters.");
            RuleFor(x => x.RoutedToSearch)
                .MaximumLength(50)
                .WithMessage("Routed To search limited to 50 characters.");
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .When(x => x.EndDate.HasValue)
                .WithMessage("You must provide a start date/time");
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .When(x => x.StartDate.HasValue)
                    .WithMessage("You must provide an end date/time")
                .GreaterThan(x => x.StartDate)
                .When(x => x.StartDate.HasValue)
                    .WithMessage("End date/time must be after start date/time");
            List<string> validSorts = new List<string>() { "", "date_asc", "title", "title_desc", "routedTo", "routedTo_desc", "submittedBy", "submittedBy_desc" };
            RuleFor(x => x.SortOrder)
                .Must(x => validSorts.Contains(x))
                .WithMessage("Must be a valid sorting value: " + string.Join(",", validSorts));
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page Size must be greater than 0.");
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page Number must be greater than 0.");

        }
    }
}
