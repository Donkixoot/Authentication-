using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data.Models;
using SimpleBlog.Interfaces;

namespace SimpleBlog.Data.Repositories
{
    /// <summary>
    ///     Репозиторий пользователей.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        ///     DbContext приложения.
        /// </summary>
        private readonly AppDbContext _appDbContext;

        /// <summary>
        ///     Репозиторий пользователей.
        /// </summary>
        /// <param name="appDbContext"> DbContext приложения. </param>
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <inheritdoc />
        public async Task AddUserAsync(User user)
        {
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
        }
    }
}