using DocRouter.Application.Common.Mappings;
using DocRouter.Common.Enums;
using DocRouter.Domain.Entities;
using System;
using System.Linq;

namespace DocRouter.Application.Submissions.Queries.GetUserSubmissions
{
    /// <summary>
    /// Data transfer class that maps entity values to display a list of simplified objects.
    /// </summary>
    public class UserSubmissionListSubmissionDto : IMapFrom<Submission>
    {
        /// <summary>
        /// The Id of the Submission.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The title of the submission.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The date the submission was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// The number of files in the submission.
        /// </summary>
        public int Files { get; set; }
        /// <summary>
        /// The name of the user to whom the submission is currently routed.
        /// </summary>
        public string CurrentlyRoutedTo { get; set; }
        /// <summary>
        /// The path to the submission folder.
        /// </summary>
        public string FolderPath { get; set; }
        /// <summary>
        /// The current status of the submission.
        /// </summary>
        public TransactionStatus CurrentStatus { get; set; }
        /// <summary>
        /// Creates a mapping between the entity class and the data transfer class.
        /// </summary>
        /// <param name="profile">A <see cref="MappingProfile"/></param>
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Submission, UserSubmissionListSubmissionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(x => x.FolderPath, opt => opt.MapFrom(c => c.FolderUri))
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(c => c.Created))
                .ForMember(x => x.CurrentStatus, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().Status))
                .ForMember(x => x.CurrentlyRoutedTo, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().RoutedTo))
                .ForMember(x => x.Files, opt => opt.MapFrom(c => c.Items.Count));
        }
    }
}
