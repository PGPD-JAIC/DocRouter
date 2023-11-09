using DocRouter.Application.Common.Mappings;
using DocRouter.Common.Enums;
using DocRouter.Domain.Entities;
using System;

namespace DocRouter.Application.Submissions
{
    /// <summary>
    /// Implementation of <see cref="IMapFrom{SubmissionTransaction}"/> that is uses as a data transfer class to relay details of a <see cref="SubmissionTransaction"/>
    /// </summary>
    public class SubmissionTransactionDto : IMapFrom<SubmissionTransaction>
    {
        /// <summary>
        /// The Id of the transaction.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Date/Time the tranaction was created.
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// The email address of the person to whom the transaction was routed.
        /// </summary>
        public string RoutedTo { get; set; }
        /// <summary>
        /// The email address of the person to who submitted the transaction.
        /// </summary>
        public string RoutedFrom { get; set; }
        /// <summary>
        /// The current status of the transaction.
        /// </summary>
        public TransactionStatus Status { get; set; }
        /// <summary>
        /// Comments associated with the transaction.
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Creates a mapping between the entity class and the Dto.
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<SubmissionTransaction, SubmissionTransactionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Created, opt => opt.MapFrom(c => c.Created))
                .ForMember(x => x.RoutedTo, opt => opt.MapFrom(c => c.RoutedTo))
                .ForMember(x => x.RoutedFrom, opt => opt.MapFrom(c => c.CreatedBy))
                .ForMember(x => x.Status, opt => opt.MapFrom(c => c.Status))
                .ForMember(x => x.Comments, opt => opt.MapFrom(c => c.Comments));
        }
    }
}
