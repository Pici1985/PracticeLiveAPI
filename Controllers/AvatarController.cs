using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeFullstackApp.Repositories.Interfaces;

namespace PracticeFullstackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        internal readonly IAvatarRepository _avatarRepository;

        public AvatarController(IAvatarRepository avatarRepository)
        {
            _avatarRepository = avatarRepository;
        }

        [Route("/avatars")]
        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> GetAllAvatars()
        {
            return Ok(_avatarRepository.GetAll());
        }
    }
}
