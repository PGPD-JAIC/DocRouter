using DocRouter.Application.Common.Mappings;
using DocRouter.Application.Common.Models;
using DocRouter.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace DocRouter.Application.Submissions.Queries.GetEditSubmissionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest{Result}"/> that handles a command to edit a submission.
    /// </summary>
    public class EditSubmissionCommand : IMapFrom<Submission>, IRequest<Result>
    {
        /// <summary>
        /// The Id of the Submission.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Title of the Submission.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description of the submission.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The name of the person to whom the submission is currently routed.
        /// </summary>
        public string CurrentlyRoutedTo { get; set; }
        /// <summary>
        /// The name of the person to whom the submission is to be re-routed.
        /// </summary>
        public string NewRoutedTo { get; set; }
        /// <summary>
        /// A list of files to add to the submission.
        /// </summary>
        public List<FileSubmissionDto> FilesToAdd { get; set; } = new List<FileSubmissionDto>();
        /// <summary>
        /// A list of files currently associated with the submission.
        /// </summary>
        public List<SubmissionItemDto> CurrentFiles { get; set; } = new List<SubmissionItemDto>();
        /// <summary>
        /// A list of Ids of files currently associated with the submission that are to be removed.
        /// </summary>
        public List<int> FilesToRemove { get; set; } = new List<int>();
        /// <summary>
        /// Creates a mapping between the Entity class and the DTO
        /// </summary>
        /// <param name="profile">A <see cref="MappingProfile"/> object.</param>
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Submission, EditSubmissionCommand>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(x => x.CurrentlyRoutedTo, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(t => t.TransactionDate).First().RoutedTo))
                .ForMember(x => x.CurrentFiles, opt => opt.MapFrom(c => c.Items));
        }
    }
}