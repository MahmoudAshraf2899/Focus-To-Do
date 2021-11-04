using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Mission,MissionDTO>().ReverseMap();
        }
    }
}
