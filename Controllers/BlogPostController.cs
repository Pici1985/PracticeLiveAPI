using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Repositories.Implementations;
using PracticeFullstackApp.Repositories.Interfaces;

namespace PracticeFullstackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        internal readonly IBlogPostRepository _iblogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            _iblogPostRepository = blogPostRepository;
        }

        [Route("/blogposts")]
        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> GetAllPosts()
        {
            IEnumerable<Post>? posts = _iblogPostRepository.GetAll();

            if (posts != null)
            {
                return Ok(posts);
            }
            return BadRequest();
        }


        [Route("/blogposts/{id}")]
        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> GetOnePost(int id)
        {
            Post? post = _iblogPostRepository.GetOne(id);

            if (post != null)
            {
                return Ok(post);
            }
            return BadRequest();
        }
    }
}
