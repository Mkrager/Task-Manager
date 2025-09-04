﻿using AutoMapper;
using Moq;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Features.Account.Queries.Authentication;
using TaskManager.Application.Profiles;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Account.Queries
{
    public class AuthenticationQueryHandlerTests
    {
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly IMapper _mapper;

        public AuthenticationQueryHandlerTests()
        {
            _mockAuthenticationService = AuthenticationServiceMock.GetAuthenticationService();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_WithValidCredentials_ReturnsAuthenticationResponse()
        {
            var handler = new AuthenticationQueryHandler(_mockAuthenticationService.Object, _mapper);

            var authenticationQuery = new AuthenticationQuery()
            {
                Password = "password",
                Email = "email@gmail.com"
            };

            var result = await handler.Handle(authenticationQuery, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<AuthenticationVm>(result);
        }
    }
}
