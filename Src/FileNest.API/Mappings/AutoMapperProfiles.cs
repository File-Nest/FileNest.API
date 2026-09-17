using AutoMapper;
using FileNest.Model.Models;
using FileNest.Web.DTO;

namespace FileNest.Web.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        { 
            CreateMap<AddUserRequestDTO, UserClass>().ReverseMap();
        }
    }
}
