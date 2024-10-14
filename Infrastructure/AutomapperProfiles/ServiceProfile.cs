using AutoMapper;
using Core.Dtos.GadgetDtos;
using Core.Dtos.UserDtos;
using Core.Entity;

namespace Infrastructure.AutomapperProfiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Gadget, GadgetDto>();
        CreateMap<Gadget, GetGadgetDto>();
        CreateMap<Gadget, AddGadgetDto>();
        CreateMap<User, UserDto>();
        CreateMap<User, AddUserDto>();
        CreateMap<User, UpdateUserDto>();
    }
}