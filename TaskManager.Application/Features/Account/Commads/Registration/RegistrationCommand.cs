using MediatR;

namespace TaskManager.Application.Features.Account.Commads.Registration
{
    public class RegistrationCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
