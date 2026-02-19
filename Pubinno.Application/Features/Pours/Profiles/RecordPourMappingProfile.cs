using AutoMapper;
using Pubinno.Application.Features.Pours.Commands.RecordPour;
using Pubinno.Domain.Entities;

namespace Pubinno.Application.Features.Pours.Profiles
{
    public class RecordPourMappingProfile : Profile
    {
        public RecordPourMappingProfile()
        {
            CreateMap<RecordPourRequest, PourEvent>()
                       .ForMember(dest => dest.StartedAt, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartedAt, DateTimeKind.Utc)))
                       .ForMember(dest => dest.EndedAt, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EndedAt, DateTimeKind.Utc)));
        }

    }
}
