using DocRouter.Application.Common.Mappings;
using DocRouter.Application.Common.Models;
using DocRouter.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace DocRouter.Application.Submissions.Queries.GetApproveTransactionDetail
{
    /// <summary>
    /// Viewmodel class used to create a transaction response.
    /// </summary>
    public class ApproveTransactionCommand : IMapFrom<SubmissionTransaction>, IRequest<Result>
    {
        /// <summary>
        /// The Id of the Submission associated with the transaction.
        /// </summary>
        public int SubmissionId { get; set; }
        /// <summary>
        /// The Id of the Transaction.
        /// </summary>
        public int TransactionId { get; set; }
        /// <summary>
        /// The comments associated with the current transaction
        /// </summary>
        public string ExistingComments { get; set; }
        /// <summary>
        /// The new comments.
        /// </summary>
        public string NewComments { get; set; }
        public string Recepient { get; set; }
        /// <summary>
        /// The title of the submission associated with the transaction.
        /// </summary>
        public string SubmissionTitle { get; set; }
        /// <summary>
        /// The list of file names associated with the transaction's submission.
        /// </summary>
        public List<string> FileNames { get; set; }
        /// <summary>
        /// The name of the person to whom the submission is currently routed.
        /// </summary>
        public string CurrentRoutedTo { get; set; }
        /// <summary>
        /// Creates a mapping between the entity and the DTO.
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<SubmissionTransaction, ApproveTransactionCommand>()
                .ForMember(x => x.SubmissionId, opt => opt.MapFrom(c => c.Submission.Id))
                .ForMember(x => x.TransactionId, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.SubmissionTitle, opt => opt.MapFrom(c => c.Submission.Title))
                .ForMember(x => x.FileNames, opt => opt.MapFrom(c => c.Submission.Items.Select(y => y.ItemName).ToList()))
                .ForMember(x => x.CurrentRoutedTo, opt => opt.MapFrom(c => c.RoutedTo))
                .ForMember(x => x.ExistingComments, opt => opt.MapFrom(c => c.Comments));
        }
    }
}
