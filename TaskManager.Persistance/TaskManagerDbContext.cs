using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Common;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistance
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditableEntity auditable)
                {
                    if (entry.State == EntityState.Added)
                        auditable.CreatedAt = now;
                    else if (entry.State == EntityState.Modified)
                        auditable.UpdatedAt = now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
