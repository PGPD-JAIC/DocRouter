using DocRouter.Application.Common.Mappings;
using DocRouter.Common.Enums;
using DocRouter.Domain.Entities;
using System;

namespace DocRouter.Application.Submissions
{
    public class SubmissionTransactionDto : IMapFrom<SubmissionTransaction>
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string RoutedTo { get; set; }
        public string RoutedFrom { get; set; }
        public TransactionStatus Status { get; set; }
        public string Comments { get; set; }
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
