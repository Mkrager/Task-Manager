using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskManager.Application.Contracts;
using TaskManager.Domain.Common;

namespace TaskManager.Persistance.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public AuditableEntitySaveChangesInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var now = DateTime.UtcNow;
            var userId = _currentUserService.UserId;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is AuditableEntity auditable)
                {
                    if (entry.State == EntityState.Added)
                        auditable.CreatedAt = now;
                    else if (entry.State == EntityState.Modified)
                        auditable.UpdatedAt = now;
                }

                if (entry.Entity is IHasUser entityWithUser && entry.State == EntityState.Added)
                {
                    if (Guid.TryParse(userId, out var parsedUserId))
                    {
                        entityWithUser.UserId = parsedUserId;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
