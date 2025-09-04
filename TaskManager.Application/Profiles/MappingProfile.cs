
using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Application.Features.Account.Commads.Registration;
using TaskManager.Application.Features.Account.Queries.Authentication;

namespace TaskManager.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationRequest, RegistrationCommand>().ReverseMap();
            CreateMap<AuthenticationRequest, AuthenticationQuery>().ReverseMap();
            CreateMap<AuthenticationResponse, AuthenticationVm>();
        }
    }
}
