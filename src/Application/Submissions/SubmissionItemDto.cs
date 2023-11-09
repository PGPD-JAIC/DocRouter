using DocRouter.Application.Common.Mappings;
using DocRouter.Domain.Entities;

namespace DocRouter.Application.Submissions
{
    /// <summary>
    /// Data transfer class that relays details about a <see cref="SubmissionItem"/>
    /// </summary>
    public class SubmissionItemDto : IMapFrom<SubmissionItem>
    {
        /// <summary>
        /// The Id of the item.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the item.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The path/uri of the item.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// The Id of the drive containing the item.
        /// </summary>
        public string DriveId { get; set; }
        /// <summary>
        /// Creates a mapping between the entity and the dto.
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<SubmissionItem, SubmissionItemDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.ItemName))
                .ForMember(x => x.Path, opt => opt.MapFrom(c => c.ItemUri))
                .ForMember(x => x.DriveId, opt => opt.MapFrom(c => c.DriveId));
        }
    }
}
