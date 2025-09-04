using TaskManager.Application.DTOs;

namespace TaskManager.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> RegisterAsync(RegistrationRequest request);
    }
}
