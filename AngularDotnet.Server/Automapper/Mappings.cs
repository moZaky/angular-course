using AngularDotnet.Core.DTOs;
using AngularDotnet.Core.Entities;
using AutoMapper;

namespace AngularDotnet.Server.Automapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Movies, MovieDTO>().ReverseMap();
        }
    }
}
