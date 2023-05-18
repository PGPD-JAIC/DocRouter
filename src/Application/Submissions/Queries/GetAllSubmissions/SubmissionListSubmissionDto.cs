using DocRouter.Application.Common.Mappings;
using DocRouter.Common.Enums;
using DocRouter.Domain.Entities;
using System;
using System.Linq;

namespace DocRouter.Application.Submissions.Queries.GetAllSubmissions
{
    public class SubmissionListSubmissionDto : IMapFrom<Submission>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public int Files { get; set; }
        public string SubmittedBy { get; set; }
        public string CurrentlyRoutedTo { get; set; }
        public string FolderPath { get; set; }
        public TransactionStatus CurrentStatus { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Submission, SubmissionListSubmissionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(x => x.FolderPath, opt => opt.MapFrom(c => c.FolderUri))
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(c => c.Created))
                .ForMember(x => x.CurrentStatus, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.TransactionDate).First().Status))
                .ForMember(x => x.CurrentlyRoutedTo, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.TransactionDate).First().RoutedTo))
                .ForMember(x => x.Files, opt => opt.MapFrom(c => c.Items.Count))
                .ForMember(x => x.SubmittedBy, opt => opt.MapFrom(c => c.CreatedBy));
        }
    }
}
