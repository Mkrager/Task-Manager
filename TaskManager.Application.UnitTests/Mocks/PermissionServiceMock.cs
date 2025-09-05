using Moq;
using TaskManager.Application.Contracts.Application;
using TaskManager.Domain.Common;

namespace TaskManager.Application.UnitTests.Mocks
{
    public class PermissionServiceMock
    {
        public static Mock<IPermissionService> GetPermissionService()
        {
            var mockService = new Mock<IPermissionService>();

            mockService.Setup(repo => repo.HasUserPermission(It.IsAny<IHasUser>(), It.IsAny<Guid>()))
                .Returns((IHasUser entity, Guid useId) => entity.UserId == useId);

            return mockService;
        }
    }
}
