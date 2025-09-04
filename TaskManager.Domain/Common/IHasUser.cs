namespace TaskManager.Domain.Common
{
    public interface IHasUser
    {
        Guid UserId { get; set; }
    }
}
