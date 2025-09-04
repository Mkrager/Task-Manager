using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistance.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TaskManagerDbContext dbContext) : base(dbContext)
        {   
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(r => r.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(r => r.Username == username);
        }
    }
}
