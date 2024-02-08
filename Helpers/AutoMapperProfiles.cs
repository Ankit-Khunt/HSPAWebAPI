using AutoMapper;
using HSPAWebAPI.Dtos;
using HSPAWebAPI.Models;

namespace HSPAWebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
