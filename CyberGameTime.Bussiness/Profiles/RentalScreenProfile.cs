using AutoMapper;
using CyberGameTime.Bussiness.Commands.RentalScreen;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.RentalScreen;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Entities.Models;
using CyberGameTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Profiles
{
    public class RentalScreenProfile : Profile
    {
        public RentalScreenProfile()
        {
            CreateMap<RentalScreens, AddRentalScreenRequest>();
            CreateMap<AddRentalScreenRequest,RentalScreens>()
                .ForMember(x => x.CreateAt, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.UpdateAt, opt => opt.MapFrom(x => DateTime.UtcNow));

            CreateMap<RentalScreens, RentalScreanDto>().ReverseMap();
        }
    }
}
