using AutoMapper;
using MongoDB.Bson;

namespace AutoMapperDemo.MappingConfigurations
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BuildingsConfigurationCreateDto, BuildingsConfiguration>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.GenerateNewId()));
        }
    }

}