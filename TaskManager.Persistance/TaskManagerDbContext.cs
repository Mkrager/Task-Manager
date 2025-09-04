using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Common;
using TaskManager.Domain.Entities;
using TaskManager.Persistance.Interceptors;

namespace TaskManager.Persistance
{
    public class TaskManagerDbContext : DbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableInterceptor;
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options, AuditableEntitySaveChangesInterceptor auditableInterceptor)
            : base(options)
        {
            _auditableInterceptor = auditableInterceptor;
        }

        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableInterceptor);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
