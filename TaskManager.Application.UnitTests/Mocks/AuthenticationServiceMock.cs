using Moq;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.UnitTests.Mocks
{
    public class AuthenticationServiceMock
    {
        public static Mock<IAuthenticationService> GetAuthenticationService()
        {
            var mockService = new Mock<IAuthenticationService>();

            mockService.Setup(service => service.AuthenticateAsync(It.IsAny<AuthenticationRequest>()))
                .ReturnsAsync(new AuthenticationResponse
                {
                    Token = "fake-token",
                    Email = "fake-email",
                    Id = Guid.NewGuid(),
                    Username = "fake-userName"
                });


            mockService.Setup(service => service.RegisterAsync(It.IsAny<RegistrationRequest>()))
                .ReturnsAsync("some-id");

            return mockService;
        }
    }
}
