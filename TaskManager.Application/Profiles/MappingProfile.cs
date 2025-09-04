
using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Application.Features.Account.Commads.Registration;
using TaskManager.Application.Features.Account.Queries.Authentication;
using TaskManager.Application.Features.Tasks.Commads.CreateTask;
using TaskManager.Application.Features.Tasks.Commads.UpdateTask;
using TaskManager.Application.Features.Tasks.Queries.GetTaskDetails;
using TaskManager.Application.Features.Tasks.Queries.GetUserTasksList;

namespace TaskManager.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationRequest, RegistrationCommand>().ReverseMap();
            CreateMap<AuthenticationRequest, AuthenticationQuery>().ReverseMap();
            CreateMap<AuthenticationResponse, AuthenticationVm>();

            CreateMap<Domain.Entities.Task, TaskListVm>().ReverseMap();
            CreateMap<Domain.Entities.Task, TaskDetailVm>().ReverseMap();

            CreateMap<Domain.Entities.Task, CreateTaskCommand>().ReverseMap();
            CreateMap<Domain.Entities.Task, UpdateTaskCommand>().ReverseMap();
        }
    }
}
