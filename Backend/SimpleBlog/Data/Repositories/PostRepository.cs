using Microsoft.EntityFrameworkCore;

using SimpleBlog.Data.Models;
using SimpleBlog.Interfaces;

namespace SimpleBlog.Data.Repositories
{
    /// <summary>
    /// Репозиторий пользователей.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        /// <summary>
        /// DbContext приложения.
        /// </summary>
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Репозиторий пользователей.
        /// </summary>
        /// <param name="appDbContext"> DbContext приложения. </param>
        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <inheritdoc />
        public async Task AddOrUpdatePostAsync(Post post)
        {
            _appDbContext.Posts.Update(post);
            await _appDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeletePostAsync(Post post)
        {
            _appDbContext.Posts.Remove(post);
            await _appDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<Post>> GetAllUserPosts(int userId)
        {
            return await _appDbContext.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            var post = await _appDbContext.Posts.FirstOrDefaultAsync(u => u.Id == postId);
            return post ?? null;
        }
    }
}