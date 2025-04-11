using AutoMapper;
using CyberGameTime.Bussiness.Commands.Conectivity;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Models;

namespace CyberGameTime.Bussiness.Profiles
{
    public class ScreenProfile : Profile
    {
        public ScreenProfile()
        {
            CreateMap<Screens, ScreenDto>().ReverseMap();
            CreateMap<ConectivityScreenQuery, Screens>().ReverseMap();
            CreateMap<AddScreanQuery, Screens>().ForMember(x => x.CreateAt, opt => opt.MapFrom(x => DateTime.UtcNow)).ReverseMap();
            CreateMap<UpdateScreenRequest, Screens>().ForMember(x => x.UpdateAt, opt => opt.MapFrom(x => DateTime.UtcNow)).ReverseMap();
        }
    }
}
