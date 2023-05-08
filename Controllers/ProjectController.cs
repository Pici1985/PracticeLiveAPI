using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeFullstackApp.Repositories.Implementations;
using PracticeFullstackApp.Repositories.Interfaces;

namespace PracticeFullstackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        internal readonly IProjectRepository _iprojectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _iprojectRepository = projectRepository;
        }

        [Route("/projects")]
        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(_iprojectRepository.GetAll());
        }
    }
}
