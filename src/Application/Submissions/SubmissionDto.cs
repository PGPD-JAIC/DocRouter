using DocRouter.Application.Common.Mappings;
using DocRouter.Common.Enums;
using DocRouter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocRouter.Application.Submissions
{
    public class SubmissionDto : IMapFrom<Submission>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FolderPath { get; set; }
        public DateTime DateCreated { get; set; }
        public TransactionStatus CurrentStatus { get; set; }
        public string CurrentlyRoutedTo { get; set; }
        public string CreatedBy { get; set; }
        public List<SubmissionTransactionDto> Transactions { get; set; }
        public List<SubmissionItem> Items { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Submission, SubmissionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(x => x.FolderPath, opt => opt.MapFrom(c => c.FolderUri))
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(c => c.Created))
                .ForMember(x => x.CurrentStatus, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().Status))
                .ForMember(x => x.CurrentlyRoutedTo, opt => opt.MapFrom(c => c.Transactions.OrderByDescending(y => y.Created).First().RoutedTo))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(c => c.CreatedBy))
                .ForMember(x => x.Transactions, opt => opt.MapFrom(c => c.Transactions))
                .ForMember(x => x.Items, opt => opt.MapFrom(c => c.Items));
        }
    }
}
