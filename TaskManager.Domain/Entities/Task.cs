using TaskManager.Domain.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Task : AuditableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public Status Status { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; } = default!;
    }
}
