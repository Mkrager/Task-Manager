using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Account.Queries.Authentication
{
    public class AuthenticationQueryHandler : IRequestHandler<AuthenticationQuery, AuthenticationVm>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationQueryHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<AuthenticationVm> Handle(AuthenticationQuery request, CancellationToken cancellationToken)
        {
            var authentication = await _authenticationService
                .AuthenticateAsync(_mapper.Map<AuthenticationRequest>(request));

            return _mapper.Map<AuthenticationVm>(authentication);
        }
    }
}
