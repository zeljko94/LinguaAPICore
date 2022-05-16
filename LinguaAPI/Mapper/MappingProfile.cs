using AutoMapper;
using LinguaAPI.Models.Dapper;
using LinguaAPI.Models.DTOs;

namespace LinguaAPI.Mapper
{
    public class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebMappingProfile());
            });

            return config;
        }
    }


    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<PredavanjeDTO, Predavanje>();
            CreateMap<Predavanje, PredavanjeDTO>();

            CreateMap<PredavanjeSudionikDTO, PredavanjeSudionik>();
            CreateMap<PredavanjeSudionik, PredavanjeSudionikDTO>();

            CreateMap<CalendarEvent, CalendarEventDTO>();
            CreateMap<CalendarEventDTO, CalendarEvent>();
        }
    }
}
