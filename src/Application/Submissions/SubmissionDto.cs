using DocRouter.Application.Common.Mappings;
using DocRouter.Common.Enums;
using DocRouter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocRouter.Application.Submissions
{
    /// <summary>
    /// Data Transfer class used to relay details of a <see cref="Submission"/>
    /// </summary>
    public class SubmissionDto : IMapFrom<Submission>
    {
        /// <summary>
        /// The Id of the submission.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Title of the submission.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description of the submission.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The path to the submission.
        /// </summary>
        public string FolderPath { get; set; }
        /// <summary>
        /// The unique Id of the submission.
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// The date the submission was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// The current status of the submission.
        /// </summary>
        public TransactionStatus CurrentStatus { get; set; }
        /// <summary>
        /// The name of the person to whom the submission is currently routed.
        /// </summary>
        public string RoutedTo { get; set; }
        /// <summary>
        /// The name of the person who submitted the last routing transaction.
        /// </summary>
        public string RoutedFrom { get; set; }
        /// <summary>
        /// The name of the person who created the submission.
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// A list of transactions associated with the submission.
        /// </summary>
        public List<SubmissionTransactionDto> Transactions { get; set; }
        /// <summary>
        /// A list of items associated with the submission.
        /// </summary>
        public List<SubmissionItem> Items { get; set; }
        /// <summary>
        /// Creates a mapping between the entity class and the DTO.
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Submission, SubmissionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(x => x.FolderPath, opt => opt.MapFrom(c => c.FolderUri))
                .ForMember(x => x.ItemId, opt => opt.MapFrom(c => c.ItemId))
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(c => c.Created))
                .ForMember(x => x.CurrentStatus, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().Status))
                .ForMember(x => x.RoutedTo, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().RoutedTo))
                .ForMember(x => x.RoutedFrom, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().CreatedBy))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(c => c.CreatedBy))
                .ForMember(x => x.Transactions, opt => opt.MapFrom(c => c.Transactions))
                .ForMember(x => x.Items, opt => opt.MapFrom(c => c.Items));
        }
    }
}
