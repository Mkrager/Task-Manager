using Microsoft.Extensions.Logging;
using Moq;

namespace TaskManager.Application.UnitTests.Mocks
{
    public class LoggerServiceMock
    {
        public static Mock<ILogger<T>> GetLoggerService<T>()
        {
            return new Mock<ILogger<T>>();
        }
    }
}