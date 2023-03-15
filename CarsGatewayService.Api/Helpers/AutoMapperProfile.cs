using AutoMapper;
using Cars.Api.Entities;
using Cars.Api.Models.Cars;
using Cars.Api.Models.Users;

namespace Cars.Api.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCarRequest, Car>()
            .ForMember(dest => dest.UserId, 
                opt => opt.MapFrom(
                    (src, dest) => dest.UserId));
        
        CreateMap<UpdateCarRequest, Car>();
        
        CreateMap<UserRequest, User>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(
                    src => PasswordHelper.HashPassword(src.Password)));
    }
}