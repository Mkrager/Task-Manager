using TaskManager.Domain.Common;

namespace TaskManager.Application.Contracts.Application
{
    public interface IPermissionService
    {
        bool HasUserPermission(IHasUser? entity, Guid userId);
    }
}