using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleBlog.Interfaces;
using SimpleBlog.Data.Models;
using SimpleBlog.Dtos;

namespace SimpleBlog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        [HttpPost("all")]
        [Authorize]
        public async Task<IActionResult> GetAllUserPosts([FromBody] EmailDto dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (user == null)
            {
                return NotFound();
            }

            var posts = await _postRepository.GetAllUserPosts(user.Id);
            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content
            }).ToList();

            return Ok(postDtos);
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> AddOrUpdatePost([FromBody] PostDto postDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(postDto.UserEmail);
            if (user == null)
            {
                return NotFound();
            }

            var post = new Post
            {
                Id = postDto.Id,
                Title = postDto.Title,
                Content = postDto.Content,
                UserId = user.Id
            };
            await _postRepository.AddOrUpdatePostAsync(post);

            return Ok();
        }

        [HttpGet("{postId}")]
        [Authorize]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            await _postRepository.DeletePostAsync(post);

            return Ok();
        }
    }
}