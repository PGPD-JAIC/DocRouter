using DocRouter.Application.Common.Mappings;
using DocRouter.Application.Common.Models;
using DocRouter.Common.Enums;
using DocRouter.Domain.Entities;
using MediatR;
using System;
using System.Linq;

namespace DocRouter.Application.Submissions.Commands.DeleteSubmission
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that handles a request to delete a submission.
    /// </summary>
    public class DeleteSubmissionCommand : IRequest<Result>, IMapFrom<Submission>
    {
        /// <summary>
        /// The id of the submission to be deleted.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The title of the submission.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description of the submission.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The comments associated with the current transaction
        /// </summary>
        public string ExistingComments { get; set; }
        /// <summary>
        /// The name of the person to whom the submission is currently routed.
        /// </summary>
        public string RoutedTo { get; set; }
        /// <summary>
        /// The date the submission was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// The name of the person who created the submission.
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// The current status of the submission.
        /// </summary>
        public TransactionStatus CurrentStatus { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Submission, DeleteSubmissionCommand>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(x => x.ExistingComments, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().Comments))
                .ForMember(x => x.RoutedTo, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().RoutedTo))
                .ForMember(x => x.CurrentStatus, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().Status))
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(c => c.Created))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(c => c.CreatedBy));                
        }
    }
}
