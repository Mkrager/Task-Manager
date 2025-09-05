using TaskManager.Application.Contracts.Application;
using TaskManager.Domain.Common;

namespace TaskManager.Application.Services
{
    public class PermissionService : IPermissionService
    {
        public bool HasUserPermission(IHasUser? entity, Guid userId)
        {
            if (entity == null)
                return false;
            return entity.UserId == userId;
        }
    }
}
