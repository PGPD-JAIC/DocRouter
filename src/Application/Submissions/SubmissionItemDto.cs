using DocRouter.Application.Common.Mappings;
using DocRouter.Domain.Entities;

namespace DocRouter.Application.Submissions
{
    public class SubmissionItemDto : IMapFrom<SubmissionItem>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<SubmissionItem, SubmissionItemDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.ItemName))
                .ForMember(x => x.Path, opt => opt.MapFrom(c => c.ItemUri));
        }
    }
}
